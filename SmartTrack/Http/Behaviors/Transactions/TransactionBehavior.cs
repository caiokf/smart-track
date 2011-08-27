using System;
using FubuCore.Binding;
using FubuMVC.Core.Behaviors;
using FubuMVC.StructureMap;
using StructureMap;

namespace SmartTrack.Web.Http.Behaviors.Transactions
{
    public class TransactionalStructureMapContainerFacility : StructureMapContainerFacility
    {
        private readonly IContainer container;

        public TransactionalStructureMapContainerFacility(IContainer container)
            : base(container)
        {
            this.container = container;
        }

        public override IActionBehavior BuildBehavior(ServiceArguments arguments, Guid behaviorId)
        {
            return new TransactionBehavior(container, arguments, behaviorId);
        }
    }

    public class TransactionBehavior : IActionBehavior
    {
        private readonly ServiceArguments arguments;
        private readonly Guid behaviorId;
        private readonly IContainer container;

        public TransactionBehavior(IContainer container, ServiceArguments arguments, Guid behaviorId)
        {
            this.container = container;
            this.arguments = arguments;
            this.behaviorId = behaviorId;
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
            var behavior = c.GetInstance<IActionBehavior>(arguments.ToExplicitArgs(), behaviorId.ToString());
            behavior.Invoke();
        }
    }
}