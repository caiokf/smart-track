using FubuMVC.Core.Behaviors;
using StructureMap;

namespace SmartTrack.Web.Http.Behaviors.Transactions
{
    public class TransactionBehavior : IActionBehavior
    {
        private readonly IContainer container;
        private readonly IActionBehavior innerBehavior;

        public TransactionBehavior(IContainer container, IActionBehavior innerBehavior)
        {
            this.container = container;
            this.innerBehavior = innerBehavior;
        }

        public void Invoke()
        {
            container.ExecuteInTransaction(c => innerBehavior.Invoke());
        }

        public void InvokePartial()
        {
            container.ExecuteInTransaction(c => innerBehavior.InvokePartial());
        }
    }
}