using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zeus.Core.Models;
using Zeus.Core.Requests;
using Zeus.Core.Responses;
using Zeus.OneSignalCoreAPI.Dto.Apps;

namespace Zeus.OneSignalCoreAPI.Services.Interfaces
{
    public interface IAppService
    {
        Task<App> CreateAppAsync(App app);
        Task<App> UpdateAppAsync(AppDto app);
        Task<App> GetAppByIdAsync(Guid appId);
        Response<List<App>> GetAllApps(PaginationFilter filter);
        Task<App> RemoveAppByIdAsync(Guid appId);
        string GenerateJwt(AppDto appDto);
    }
}
