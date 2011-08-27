using System;
using NHibernate;

namespace SmartTrack.Web.Http.Behaviors.Transactions
{
    public interface ITransactionBoundary : IDisposable
    {
        void Start();
        void Commit();
        void Rollback();
        ISession Session { get; }
    }

    public class NHibernateTransactionBoundary : ITransactionBoundary
    {
        private readonly ISessionFactory sessionFactory;
        private bool isInitialized;
        private ISession session;
        private ITransaction transaction;

        public NHibernateTransactionBoundary(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        public ISession Session
        {
            get
            {
                ensure_initialized();
                return session;
            }
        }

        public bool IsDisposed { get; private set; }

        public void Start()
        {
            session = sessionFactory.OpenSession();
            session.FlushMode = FlushMode.Commit;
            transaction = session.BeginTransaction();
            isInitialized = true;
        }

        public void Commit()
        {
            should_not_be_disposed();
            ensure_initialized();
            transaction.Commit();
        }

        public void Rollback()
        {
            should_not_be_disposed();
            ensure_initialized();
            transaction.Rollback();

            transaction = session.BeginTransaction();
        }

        public void Dispose()
        {
            IsDisposed = true;
            if (transaction != null) transaction.Dispose();
            if (session != null) session.Dispose();
        }

        private void should_not_be_disposed()
        {
            if (!IsDisposed) return;
            throw new ObjectDisposedException("NHibernateTransactionBoundary");
        }

        private void ensure_initialized()
        {
            if (!isInitialized)
            {
                throw new InvalidOperationException("An attempt was made to access the database session outside of a transaction. Please make sure all access is made within an initialized transaction boundary.");
            }
        }
    }
}