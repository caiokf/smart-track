using System;
using StructureMap;

namespace SmartTrack.Web.Http.Behaviors.Transactions
{
    public class TransactionProcessor
    {
        private readonly IContainer container;

        public TransactionProcessor(IContainer container)
        {
            this.container = container;
        }

        public void ExecuteInTransaction(Action<IContainer> action)
        {
            using (var nestedContainer = container.GetNestedContainer())
            using (var boundary = nestedContainer.GetInstance<ITransactionBoundary>())
            {
                boundary.Start();
                try
                {
                    action(nestedContainer);
                    boundary.Commit();
                }
                catch (Exception)
                {
                    boundary.Rollback();
                    throw;
                }
            }
        }
    }
}