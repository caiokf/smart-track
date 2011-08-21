using System;
using StructureMap;

namespace SmartTrack.Web.Http.Behaviors.Transactions
{
    public static class TransactionalContainerExtensions
    {
        public static void ExecuteInTransaction(this IContainer container, Action<IContainer> action)
        {
            container.GetInstance<TransactionProcessor>().ExecuteInTransaction(action);
        }
    }
}