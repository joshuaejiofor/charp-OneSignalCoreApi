using Zeus.Core.UnitOfWork;
using System.Threading.Tasks;
using Zeus.Core.Repositories;
using Zeus.EntityFrameworkCore.Repositories;

namespace Zeus.EntityFrameworkCore
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OneSignalDbContext _context;

        public UnitOfWork(OneSignalDbContext oneSignalDbContext)
        {
            _context = oneSignalDbContext;
            AppsRepository = new AppRepository(_context);
        }

        public IAppRepository AppsRepository { get; private set; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
