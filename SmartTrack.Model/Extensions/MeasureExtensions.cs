using System.Collections.Generic;
using System.Linq;
using SmartTrack.Model.Measures;

namespace SmartTrack.Model.Extensions
{
    public static class MeasureExtensions
    {
        public static Measure WithName(this IEnumerable<Measure> measures, string name)
        {
            if (name == null)
                return null;

            return measures.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
        }

        public static Group WithName(this IEnumerable<Group> groups, string name)
        {
            if (name == null)
                return null;

            return groups.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
        }
    }
}