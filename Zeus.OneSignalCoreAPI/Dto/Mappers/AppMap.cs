using AutoMapper;
using Zeus.Core.Models;
using Zeus.OneSignalCoreAPI.Dto.Apps;

namespace Zeus.OneSignalCoreAPI.Dto.Mappers
{
    public class AppMap : Profile
    {
        public AppMap()
        {
            CreateMap<App, AppDto>();

            CreateMap<AppDto, App>();
        }
    }
}
