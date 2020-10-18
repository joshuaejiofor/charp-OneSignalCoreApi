using AutoMapper;
using Zeus.Core.Models;

namespace Zeus.OneSignalCoreAPI.Dto.Mappers
{
    public class AppMap : Profile
    {
        public AppMap()
        {
            CreateMap<App, AppMap>();

            CreateMap<AppMap, App>();
        }
    }
}
