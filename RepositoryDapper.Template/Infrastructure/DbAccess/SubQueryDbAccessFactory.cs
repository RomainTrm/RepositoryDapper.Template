using System;

namespace RepositoryDapper.Template.Infrastructure.DbAccess
{
    public sealed class SubQueryDbAccessFactory : IDisposable
    {
        private readonly ITransactionalDbAccess _parent;

        public SubQueryDbAccessFactory(ITransactionalDbAccess parent)
        {
            _parent = parent;
        }

        public ITransactionalDbAccess Build() => new EmbeddedTransactionalDbAccess(_parent);

        #region Disposable pattern

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~SubQueryDbAccessFactory()
        {
            Dispose(false);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing) return;
            _parent?.Dispose();
        }

        #endregion
    }
}