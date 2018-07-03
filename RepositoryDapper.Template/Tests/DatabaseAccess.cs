using NUnit.Framework;
using RepositoryDapper.Template.Infrastructure.DbAccess;

namespace RepositoryDapper.Template.Tests
{
    public static class DatabaseAccess
    {
        private const string ConnectionString = ""; //TODO : provide  a database's connection string dedicated for tests

        public static TransactionRootDbAccess GetNewTestSession()
        {
            var context = new TransactionRootDbAccess(ConnectionString);
            context.StartTransaction();
            return context;
        }

        public static FakeEmbeddedTransactionalDbAccess GetRepositoryContext(this ITransactionalDbAccess dbSession)
        {
            return new FakeEmbeddedTransactionalDbAccess(dbSession);
        }

        [SetUpFixture]
        [SingleThreaded]
        public class SetupTests
        {
            [OneTimeSetUp]
            public static void Init()
            {
                // TODO : Init database here
            }

            [OneTimeTearDown]
            public static void Cleanup()
            {
                // TODO : Delete database here
            }
        }
    }
}