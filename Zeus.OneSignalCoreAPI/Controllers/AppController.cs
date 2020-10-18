using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Zeus.Core.Helpers.Interface;
using Zeus.Core.Responses;
using Zeus.Core.UnitOfWork;

namespace Zeus.OneSignalCoreAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {
    }
}
