using System;
using System.Collections.Generic;
using RepositoryDapper.Template.Infrastructure.DbAccess;

namespace RepositoryDapper.Template.Infrastructure
{
    public abstract class RepositoryBase
    {
        private readonly ILogger _logger;
        private readonly Func<ITransactionalDbAccess> _getDbAccess;

        protected RepositoryBase(ILogger logger, Func<ITransactionalDbAccess> getDbAccess)
        {
            _logger = logger;
            _getDbAccess = getDbAccess;
        }

        protected void Write(string query, object param = null)
        {
            using (var dbAccess = _getDbAccess())
            {
                try
                {
                    dbAccess.StartTransaction();
                    dbAccess.Execute(query, param);
                    dbAccess.Commit();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, $"Erreur lors de l'écriture sur {GetType().Name} : {ex.Message}");
                    dbAccess.Rollback();
                    throw;
                }
            }
        }

        protected void WriteMultipleQueries((string query, object param)[] writes)
        {
            using (var dbAccess = _getDbAccess())
            {
                try
                {
                    dbAccess.StartTransaction();
                    Array.ForEach(writes, write => dbAccess.Execute(write.query, write.param));
                    dbAccess.Commit();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, $"Erreur lors de l'écriture sur {GetType().Name} : {ex.Message}");
                    dbAccess.Rollback();
                    throw;
                }
            }
        }

        protected IEnumerable<TDbModel> Query<TDbModel>(string query, object param = null)
        {
            using (var dbAccess = _getDbAccess())
            {
                return dbAccess.Query<TDbModel>(query, param);
            }
        }

        protected TDbModel QuerySingle<TDbModel>(string query, object param = null)
        {
            using (var dbAccess = _getDbAccess())
            {
                return dbAccess.QuerySingle<TDbModel>(query, param);
            }
        }

        protected TDbModel QuerySingleOrDefault<TDbModel>(string query, object param = null)
        {
            using (var dbAccess = _getDbAccess())
            {
                return dbAccess.QuerySingleOrDefault<TDbModel>(query, param);
            }
        }

        protected TDbModel QueryFirstOrDefault<TDbModel>(string query, object param = null)
        {
            using (var dbAccess = _getDbAccess())
            {
                return dbAccess.QueryFirstOrDefault<TDbModel>(query, param);
            }
        }
    }
}
