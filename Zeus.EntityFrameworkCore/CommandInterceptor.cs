using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Zeus.EntityFrameworkCore
{
    public class CommandInterceptor : DbCommandInterceptor
    {
        public override async Task<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result, CancellationToken cancellationToken = default)
        {
            return await base.ReaderExecutingAsync(command, eventData, result, cancellationToken);
        }
    }
}
