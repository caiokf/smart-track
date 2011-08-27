using System;
using FubuCore.Binding;
using FubuMVC.Core.Behaviors;
using FubuMVC.StructureMap;
using StructureMap;

namespace SmartTrack.Web.Http.Behaviors.Transactions
{
    //public class TransactionalStructureMapContainerFacility : StructureMapContainerFacility
    //{
    //    private readonly IContainer container;

    //    public TransactionalStructureMapContainerFacility(IContainer container) : base(container)
    //    {
    //        this.container = container;
    //    }

    //    public override IActionBehavior BuildBehavior(ServiceArguments arguments, Guid behaviorId)
    //    {
    //        return new TransactionBehavior(container, arguments, behaviorId);
    //    }
    //}

    public class TransactionBehavior : IActionBehavior
    {
        private readonly IContainer container;
        public IActionBehavior InnerBehavior { get; set; }

        public TransactionBehavior(IContainer container)
        {
            this.container = container;
        }

        public void Invoke()
        {
            container.ExecuteInTransaction(InvokeRequestedBehavior);
        }

        public void InvokePartial()
        {
            InvokeRequestedBehavior(container);
        }

        private void InvokeRequestedBehavior(IContainer c)
        {
            InnerBehavior.Invoke();
        }
    }
}