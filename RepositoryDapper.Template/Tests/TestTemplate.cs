using System;
using System.Data;
using Moq;
using NUnit.Framework;
using RepositoryDapper.Template.Infrastructure;
using RepositoryDapper.Template.Infrastructure.DbAccess;

namespace RepositoryDapper.Template.Tests
{
    [TestFixture, SingleThreaded, NonParallelizable]
    [NCrunch.Framework.Isolated, NCrunch.Framework.Serial]
    public sealed class TestTemplate : IDisposable
    {
        private TemplateRepository _repository;
        private TransactionRootDbAccess _testDbAccess;
        private FakeEmbeddedTransactionalDbAccess _repositoryDbAccess;

        private IDbConnection Connexion => _testDbAccess.Connexion;
        private IDbTransaction Transaction => _testDbAccess.Transaction;

        [SetUp]
        public void Init()
        {
            _testDbAccess = DatabaseAccess.GetNewTestSession();
            _repositoryDbAccess = _testDbAccess.GetRepositoryContext();

            _repository = new TemplateRepository(Mock.Of<ILogger>(), () => _repositoryDbAccess);
        }

        [TearDown]
        public void Close()
        {
            _testDbAccess.Rollback();
            _testDbAccess.Dispose();
        }

        [Test]
        public void SomeTest()
        {
            // TODO : replace this test
        }

        #region Disposable pattern

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~TestTemplate()
        {
            Dispose(false);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing) return;
            _testDbAccess?.Dispose();
        }

        #endregion
    }
}
