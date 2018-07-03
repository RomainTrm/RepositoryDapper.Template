namespace RepositoryDapper.Template.Infrastructure.DbAccess
{
    public interface ITransactionalDbAccess : IReadDbAccess
    {
        void StartTransaction();

        void Commit();

        void Rollback();

        void Execute(string query, object param = null);
    }
}