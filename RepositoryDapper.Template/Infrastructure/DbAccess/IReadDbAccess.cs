using System;
using System.Collections.Generic;

namespace RepositoryDapper.Template.Infrastructure.DbAccess
{
    public interface IReadDbAccess : IDisposable
    {
        IEnumerable<TDbModel> Query<TDbModel>(string query, object param = null);

        TDbModel QuerySingle<TDbModel>(string query, object param = null);

        TDbModel QuerySingleOrDefault<TDbModel>(string query, object param = null);

        TDbModel QueryFirstOrDefault<TDbModel>(string query, object param = null);
    }
}