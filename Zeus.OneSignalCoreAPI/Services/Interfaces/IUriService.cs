using System;
using Zeus.Core.Requests;

namespace Zeus.OneSignalCoreAPI.Services.Interfaces
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);
    }
}
