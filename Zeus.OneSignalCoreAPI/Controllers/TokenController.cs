using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Zeus.Core.Responses;
using Zeus.Core.UnitOfWork;
using Zeus.OneSignalCoreAPI.Dto.Apps;
using Zeus.OneSignalCoreAPI.Services.Interfaces;

namespace Zeus.OneSignalCoreAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly IAppService _appService;

        public TokenController(ILogger<TokenController> logger,
                                    IUnitOfWork unitOfWork,
                                    IMapper mapper,
                                    IAppService appService,
                                    IMemoryCache memoryCache)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _appService = appService;
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// Get token by appId (No auth)
        /// </summary>
        // GET api/v1/<TokenController>/92911750-242d-4260-9e00-9d9034f139ce
        [HttpGet("{appId}")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, Type = typeof(Response<string>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Response<string>))]
        public async Task<IActionResult> Get(Guid appId)
        {
            try
            {
                var cacheKey = appId;
                if (!_memoryCache.TryGetValue(cacheKey, out string token))
                {
                    var app = await _unitOfWork.AppsRepository.SingleOrDefaultAsync(c => c.Id == appId);
                    if (app == null)
                    {
                        var error = $"App with ID {appId} not found, returning 404";
                        _logger.LogDebug(error);
                        return NotFound(new Response<string>("", error, 404));
                    }

                    if (app.Basic_auth_key == null || app.Basic_auth_key.Length < 2)
                    {
                        var customerDto = _mapper.Map<AppDto>(app);
                        app.Basic_auth_key = _appService.GenerateJwt(customerDto);
                        await _unitOfWork.CompleteAsync();
                    }
                    token = app.Basic_auth_key;

                    var cacheExpirationOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddDays(1),
                        Priority = CacheItemPriority.Normal,
                        SlidingExpiration = TimeSpan.FromHours(1)
                    };

                    _memoryCache.Set(cacheKey, token, cacheExpirationOptions);
                }

                _logger.LogInformation($"{token} fetched successfully for appId {appId}");
                return Ok(new Response<String>(token));
            }
            catch (Exception ex)
            {
                var error = $"Failed to fetch app for id {appId}";
                _logger.LogError(ex, error);
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>("", error, 500));
            }
        }

    }
}

