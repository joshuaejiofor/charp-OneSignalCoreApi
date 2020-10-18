using System;
using System.Threading.Tasks;
using Zeus.Core.Repositories;

namespace Zeus.Core.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IAppRepository AppsRepository { get; }

        Task<int> CompleteAsync();
    }
}
