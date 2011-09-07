using System;
using Moq;
using NUnit.Framework;
using SharpTestsEx;
using SmartTrack.Web.Http.Behaviors.Transactions;
using StructureMap;

namespace SmartTrack.Tests.Unit.Transactions
{
    [TestFixture]
    public class TransactionProcessorTests
    {
        private TransactionProcessor transactionProcessor;
        private Mock<ITransactionBoundary> transaction;
        private Mock<IContainer> container;

        [SetUp]
        public void setup()
        {
            transaction = new Mock<ITransactionBoundary>();
            container = new Mock<IContainer>();
            container.Setup(x => x.GetInstance<ITransactionBoundary>()).Returns(transaction.Object);
            transactionProcessor = new TransactionProcessor(container.Object);
        }

        [Test]
        [Ignore]
        public void should_rollback_in_case_of_exception_and_rethrow()
        {
            var innerException = new Exception("inner");
            var outerException = new Exception("outer", innerException);

            var rethrow = Assert.Throws<Exception>(() => 
                transactionProcessor.ExecuteInTransaction(c => { throw outerException; })
            );

            rethrow.Should().Be.EqualTo(outerException);
            rethrow.InnerException.Should().Be.EqualTo(innerException);

            container.Verify(x => x.GetInstance<ITransactionBoundary>());
            transaction.Verify(x => x.Start());
            transaction.Verify(x => x.Rollback());
        }

        [Test]
        [Ignore]
        public void should_commit_in_the_end_of_execution()
        {
            transactionProcessor.ExecuteInTransaction(c => { });

            container.Verify(x => x.GetInstance<ITransactionBoundary>());
            transaction.Verify(x => x.Start());
            transaction.Verify(x => x.Commit());
        }
    }
}