using RepositoryDapper.Template.Infrastructure.DbAccess;

namespace RepositoryDapper.Template.Tests
{
    public sealed class FakeEmbeddedTransactionalDbAccess : EmbeddedTransactionalDbAccess
    {
        public FakeEmbeddedTransactionalDbAccess(ITransactionalDbAccess parent)
            : base(parent)
        {
        }

        public bool Commited { get; private set; }

        public bool RolledBack { get; private set; }

        public override void Commit()
        {
            Commited = true;
        }

        public override void Rollback()
        {
            RolledBack = true;
        }

        public void CleanTransactionHistory()
        {
            Commited = false;
            RolledBack = false;
        }
    }
}