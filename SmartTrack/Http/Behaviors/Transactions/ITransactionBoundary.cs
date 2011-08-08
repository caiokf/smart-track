using System;
using NHibernate;

namespace SmartTrack.Web.Http.Behaviors
{
    public interface ITransactionBoundary : IDisposable
    {
        void Start();
        void Commit();
        void Rollback();
        ISession Session { get; }
    }
}