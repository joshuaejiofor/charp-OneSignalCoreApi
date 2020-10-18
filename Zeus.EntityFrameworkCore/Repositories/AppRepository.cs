using Zeus.Core.Models;
using Zeus.Core.Repositories;

namespace Zeus.EntityFrameworkCore.Repositories
{
    public class AppRepository : Repository<App>, IAppRepository
    {
        public AppRepository(OneSignalDbContext context) : base(context)
        {

        }
    }
}
