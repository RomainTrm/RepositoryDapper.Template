using System;
using System.Collections.Generic;

namespace RepositoryDapper.Template.Infrastructure.DbAccess
{
    public class EmbeddedTransactionalDbAccess : ITransactionalDbAccess
    {
        private readonly ITransactionalDbAccess _parent;

        public EmbeddedTransactionalDbAccess(ITransactionalDbAccess parent)
        {
            _parent = parent;
        }

        public IEnumerable<TDbModel> Query<TDbModel>(string query, object param = null)
            => _parent.Query<TDbModel>(query, param);

        public TDbModel QuerySingle<TDbModel>(string query, object param = null)
            => _parent.QuerySingle<TDbModel>(query, param);

        public TDbModel QuerySingleOrDefault<TDbModel>(string query, object param = null)
            => _parent.QuerySingleOrDefault<TDbModel>(query, param);

        public TDbModel QueryFirstOrDefault<TDbModel>(string query, object param = null)
            => _parent.QueryFirstOrDefault<TDbModel>(query, param);

        public void Execute(string query, object param = null)
            => _parent.Execute(query, param);

        public void StartTransaction()
        {
            // this is parent's responsability
        }

        public virtual void Commit()
        {
            // this is parent's responsability
        }

        public virtual void Rollback()
        {
            // this is parent's responsability
        }

        #region Dispose pattern

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~EmbeddedTransactionalDbAccess()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            // EmbeddedTransactionDbAccess n'est pas propriétaire de la transaction
        }

        #endregion
    }
}