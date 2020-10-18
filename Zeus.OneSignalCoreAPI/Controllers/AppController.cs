using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Zeus.Core.Helpers.Interface;
using Zeus.Core.Models;
using Zeus.Core.Requests;
using Zeus.Core.Responses;
using Zeus.OneSignalCoreAPI.Dto.Apps;
using Zeus.OneSignalCoreAPI.Exceptions;
using Zeus.OneSignalCoreAPI.Helpers;
using Zeus.OneSignalCoreAPI.Responses;
using Zeus.OneSignalCoreAPI.Services.Interfaces;

namespace Zeus.OneSignalCoreAPI.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IAppService _appService;
        //private readonly IInputValidator _inputValidator;
        private readonly IUriService _uriService;
        private readonly IMemoryCache _memoryCache;
        private readonly IMapper _mapper;

        public AppController(ILogger<AppController> logger,
                                    //IInputValidator inputValidator,
                                    IUriService uriService,
                                    IMemoryCache memoryCache,
                                    IMapper mapper,
                                    IAppService appService)
        {
            _logger = logger;
            _appService = appService;
            //_inputValidator = inputValidator;
            _uriService = uriService;
            _memoryCache = memoryCache;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all apps - with pagination (Auth token required)
        /// </summary>
        // GET: api/v1/<AppController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<List<AppDto>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Response<string>))]
        public IActionResult Get([FromQuery] PaginationFilter filter)
        {
            try
            {
                var route = Request.Path.Value;
                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

                var cacheKey = new { filter };
                if (!_memoryCache.TryGetValue(cacheKey, out Response<List<App>> result))
                {
                    result = _appService.GetAllApps(validFilter);

                    var cacheExpirationOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddHours(3),
                        Priority = CacheItemPriority.Normal,
                        SlidingExpiration = TimeSpan.FromMinutes(5)
                    };

                    _memoryCache.Set(cacheKey, result, cacheExpirationOptions);
                }
                var appDtoList = _mapper.Map<List<AppDto>>(result.Data);
                var pagedReponse = PaginationHelper.CreatePagedReponse(appDtoList, validFilter, result.TotalRecords, _uriService, route);
                return Ok(pagedReponse);
            }
            catch (Exception ex)
            {
                var error = $"Failed to fetch all active app";

                _logger.LogError(ex, error);
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>("", error, 400));
            }
        }

        /// <summary>
        /// Get app detail by id (Auth token required)
        /// </summary>
        /// <remarks>
        /// Get the App details with Id
        /// </remarks> 
        /// <param name="appId">This id is unique/primary key of app </param> 
        /// <returns object="">App details with id </returns>
        /// <response code="200"></response>

        // GET api/v1/<AppController>/92911750-242d-4260-9e00-9d9034f139ce
        [HttpGet("{appId}")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, Type = typeof(Response<AppDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Response<string>))]
        public async Task<IActionResult> Get(Guid appId)
        {
            try
            {
                //if (!_inputValidator.IsValid(appId))
                //{
                //    var error = $"Get: Invalid appid '{appId}' received, returning 400";

                //    _logger.LogWarning(error);
                //    return BadRequest(new Response<string>("", error, 400));
                //}

                var cacheKey = new { appId };
                if (!_memoryCache.TryGetValue(cacheKey, out App result))
                {
                    result = await _appService.GetAppByIdAsync(appId);
                    if (result == null)
                    {
                        var error = $"App with ID {appId} not found, returning 404";

                        _logger.LogDebug(error);
                        return NotFound(new Response<string>("", error, 404));
                    }

                    var cacheExpirationOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddHours(3),
                        Priority = CacheItemPriority.Normal,
                        SlidingExpiration = TimeSpan.FromMinutes(5)
                    };

                    _memoryCache.Set(cacheKey, result, cacheExpirationOptions);
                }

                var appDto = _mapper.Map<AppDto>(result);

                _logger.LogInformation($"{appDto} fetched successfully for appId {appId}");
                return Ok(new Response<AppDto>(appDto));
            }
            catch (Exception ex)
            {
                var error = $"Failed to fetch app for id {appId}";
                _logger.LogError(ex, error);
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>("", error, 500));
            }
        }

        /// <summary>
        /// Create new app(Auth token required)
        /// </summary>
        // POST api/v1/<AppController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Response<AppDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<string>))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(Response<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Response<string>))]
        public async Task<IActionResult> Post([FromBody] AppDto appDto)
        {
            try
            {
                var app = _mapper.Map<App>(appDto);
                var result = await _appService.CreateAppAsync(app);

                var newApp = _mapper.Map<AppDto>(result);
                return Created($@"api/v1/app/{result.Id}", new Response<AppDto>(newApp));
            }
            catch (DuplicateException ex)
            {
                var error = $"Failed to create duplicate app for {JsonSerializer.Serialize(appDto)}";
                _logger.LogError(ex, error);
                return StatusCode(StatusCodes.Status409Conflict, new Response<string>("", error, 409));
            }
            catch (Exception ex)
            {
                var error = $"Failed to create app for {JsonSerializer.Serialize(appDto)}";
                _logger.LogError(ex, error);
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>("", error, 500));
            }
        }

        /// <summary>
        /// Update existing app (Auth token required)
        /// </summary>
        // PUT api/v1/<AppController>/92911750-242d-4260-9e00-9d9034f139ce
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<AppDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<string>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Response<string>))]
        public async Task<IActionResult> Put([FromBody] AppDto appDto)
        {
            try
            {
                var result = await _appService.UpdateAppAsync(appDto);
                var updatedAppDto = _mapper.Map<AppDto>(result);

                return Ok(new Response<AppDto>(updatedAppDto));
            }
            catch (NullReferenceException ex)
            {
                var error = $"Failed to find app for {JsonSerializer.Serialize(appDto)}";
                _logger.LogError(ex, error);
                return StatusCode(StatusCodes.Status404NotFound, new Response<string>("", error, 404));
            }
            catch (Exception ex)
            {
                var error = $"Failed to update app for {JsonSerializer.Serialize(appDto)}";
                _logger.LogError(ex, error);
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>("", error, 500));
            }

        }

        /// <summary>
        /// Logical delete - Removes app by id (Auth token required)
        /// </summary>
        // DELETE api/v1/<AppController>/92911750-242d-4260-9e00-9d9034f139ce
        [HttpDelete("{appId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<string>))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(Response<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<string>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Response<string>))]
        public async Task<IActionResult> Delete(Guid appId)
        {
            try
            {
                //if (!_inputValidator.IsValid(appId))
                //{
                //    var error = $"Get: Invalid appId '{appId}' received, returning 400";
                //    _logger.LogWarning(error);
                //    return BadRequest(new Response<string>("", error, 400));
                //}

                var result = await _appService.RemoveAppByIdAsync(appId);
                if (result == null)
                {
                    var error = $"App with ID {appId} not found, returning 404";
                    _logger.LogDebug(error);
                    return NotFound(new Response<string>("", error, 404));
                }

                return Ok(new Response<string>("", "", 200, true, 0));
            }
            catch (AlreadyDeletedException ex)
            {
                var error = $"App already deleted for id {appId}";
                _logger.LogInformation(ex, error);
                return StatusCode(StatusCodes.Status204NoContent, new Response<string>("", error, 204));
            }
            catch (Exception ex)
            {
                var error = $"Failed to delete app for id {appId}";
                _logger.LogError(ex, error);
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<string>("", error, 500));
            }

        }
    }
}
