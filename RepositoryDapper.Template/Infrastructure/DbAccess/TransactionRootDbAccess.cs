using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace RepositoryDapper.Template.Infrastructure.DbAccess
{
    public sealed class TransactionRootDbAccess : ITransactionalDbAccess
    {
        private readonly string _connexionString;

        private IDbConnection _connexion;

        public TransactionRootDbAccess(string connexionString)
        {
            _connexionString = connexionString;
        }

        public IDbConnection Connexion
        {
            get
            {
                if (_connexion == null || _connexion.State == ConnectionState.Closed)
                {
                    OpenConnexion();
                }
                return _connexion;
            }
        }

        private void OpenConnexion()
        {
            _connexion = new SqlConnection(_connexionString);
            _connexion.Open();
        }

        public IEnumerable<TDbModel> Query<TDbModel>(string query, object param = null)
            => Connexion.Query<TDbModel>(query, param, Transaction);

        public TDbModel QuerySingle<TDbModel>(string query, object param = null)
            => Connexion.QuerySingle<TDbModel>(query, param, Transaction);

        public TDbModel QuerySingleOrDefault<TDbModel>(string query, object param = null)
            => Connexion.QuerySingleOrDefault<TDbModel>(query, param, Transaction);

        public TDbModel QueryFirstOrDefault<TDbModel>(string query, object param = null)
            => Connexion.QueryFirstOrDefault<TDbModel>(query, param, Transaction);

        public void Execute(string query, object param = null)
            => Connexion.Execute(query, param, Transaction);

        public void StartTransaction()
        {
            Transaction = Connexion.BeginTransaction();
        }

        public IDbTransaction Transaction { get; private set; }

        public void Commit()
        {
            Transaction.Commit();
        }

        public void Rollback()
        {
            Transaction.Rollback();
        }

        public void Dispose()
        {
            Connexion?.Dispose();
            Transaction?.Dispose();
        }
    }
}