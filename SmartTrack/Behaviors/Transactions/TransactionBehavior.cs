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

    //    public TransactionalStructureMapContainerFacility(IContainer container)
    //        : base(container)
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
        private readonly Func<IActionBehavior> innerBehavior;

        public TransactionBehavior(IContainer container, Func<IActionBehavior> innerBehavior)
        {
            this.container = container;
            this.innerBehavior = innerBehavior;
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
            innerBehavior().Invoke();
        }
    }
}