using System;

namespace RepositoryDapper.Template.Infrastructure
{
    public interface ILogger
    {
        void Error(Exception exception, string message);
    }
}