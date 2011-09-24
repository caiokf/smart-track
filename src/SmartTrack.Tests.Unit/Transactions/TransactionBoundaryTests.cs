using Moq;
using NHibernate;
using NUnit.Framework;
using SmartTrack.Web.Http.Behaviors.Transactions;

namespace SmartTrack.Tests.Unit.Transactions
{
    [TestFixture]
    public class TransactionBoundaryTests
    {
        private Mock<ISessionFactory> sessionFactory;
        private Mock<ISession> session;
        private NHibernateTransactionBoundary transactionBoundary;

        [SetUp]
        public void setup()
        {
            session = new Mock<ISession>();
            sessionFactory = new Mock<ISessionFactory>();
            sessionFactory.Setup(x => x.OpenSession()).Returns(session.Object);

            transactionBoundary = new NHibernateTransactionBoundary(sessionFactory.Object);
        }

        [Test]
        public void when_started_should_open_session_and_begin_transaction()
        {
            transactionBoundary.Start();

            sessionFactory.Verify(x => x.OpenSession());
            session.Verify(x => x.BeginTransaction());
        }

        [Test]
        public void commit_should_call_on_transaction()
        {
            var transaction = new Mock<ITransaction>();
            session.Setup(x => x.BeginTransaction()).Returns(transaction.Object);
            transactionBoundary.Start();

            transactionBoundary.Commit();

            transaction.Verify(x => x.Commit());
        }
    }
}