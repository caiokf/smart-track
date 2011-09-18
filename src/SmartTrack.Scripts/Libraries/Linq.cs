using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace SmartTrack.Scripts.Libraries
{
    [IgnoreNamespace]
    internal static class Linq
    {
        public static LinqObject From(object list)
        {
            LinqObject value = new LinqObject(list);
            return value;
        }
    }

    [IgnoreNamespace]
    internal class LinqObject
    {
        readonly Array list;
        ArrayItemMapCallback select;
        ArrayItemFilterCallback where;
        CompareCallback orderby;

        public LinqObject(object list)
        {
            this.list = Array.ToArray(list);
        }

        public LinqObject Where(ArrayItemFilterCallback where)
        {
            this.where = where;
            return this;
        }

        public LinqObject OrderBy(CompareCallback orderby)
        {
            this.orderby = orderby;
            return this;
        }

        [AlternateSignature]
        public extern LinqObject Select();

        [AlternateSignature]
        public LinqObject Select(ArrayItemMapCallback select)
        {
            if (select != null)
            {
                this.select = select;
            }
            return this;
        }

        public int Count()
        {
            return ToArray().Length;
        }

        public object FirstOrDefault()
        {
            ArrayList tempList = new ArrayList();

            // filter by 'where'
            foreach (object item in list)
            {
                if (where == null || where(item))
                {
                    tempList.Add(item);
                    if (orderby == null)
                    {
                        break;
                    }
                }
            }

            // perform 'orderby'
            if (orderby != null)
            {
                tempList.Sort(orderby);
            }

            // process 'select'
            if (select != null)
            {
                for (int i = 0; i < tempList.Count; i++)
                {
                    tempList[i] = select(tempList[i]);
                }
            }

            return tempList.Count > 0 ? tempList[0] : null;
        }

        public Array ToArray()
        {
            return (Array)ToList();
        }

        public ArrayList ToList()
        {
            ArrayList value = new ArrayList();

            // filter by 'where'
            foreach (object item in list)
            {
                if (where == null || where(item))
                {
                    value.Add(item);
                }
            }

            // perform 'orderby'
            if (orderby != null)
            {
                value.Sort(orderby);
            }

            // process 'select'
            if (select != null)
            {
                for (int i = 0; i < value.Count; i++)
                {
                    value[i] = select(value[i]);
                }
            }

            return value;
        }
    }
}