using System;
using RepositoryDapper.Template.Infrastructure.DbAccess;

namespace RepositoryDapper.Template.Infrastructure
{
    public class TemplateRepository : RepositoryBase
    {
        public static TemplateRepository Create(string connectionString, ILogger logger)
            => new TemplateRepository(logger, () => new TransactionRootDbAccess(connectionString));

        internal TemplateRepository(ILogger logger, Func<ITransactionalDbAccess> getDbAccess) 
            : base(logger, getDbAccess)
        {
        }
    }
}