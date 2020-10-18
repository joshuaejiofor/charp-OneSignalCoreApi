using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Zeus.Application;
using Zeus.Core.Models;
using Zeus.Core.UnitOfWork;
using Zeus.OneSignalCoreAPI.Dto.Apps;
using Zeus.OneSignalCoreAPI.Exceptions;
using Zeus.OneSignalCoreAPI.Services.Interfaces;
using Zeus.Core.Requests;
using Zeus.Core.Responses;

namespace Zeus.OneSignalCoreAPI.Services
{
    public class AppService : AppServiceBase, IAppService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public AppService(IUnitOfWork unitOfWork,
                                IConfiguration configuration,
                                ILogger<AppService> logger) : base(logger)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<App> CreateAppAsync(App app)
        {
            var item = await _unitOfWork.AppsRepository.FirstOrDefaultAsync(c => c.Name == app.Name &&
                                                                                        c.Players == app.Players &&
                                                                                        c.Site_name == app.Site_name &&
                                                                                        c.Gcm_key == app.Gcm_key &&
                                                                                        c.Apns_env == app.Apns_env &&
                                                                                        c.Messageable_players == app.Messageable_players);
            if (item != null) throw new DuplicateException();

            app.Basic_auth_key = GenerateJwt(new AppDto { Id = app.Id, Name = app.Name});
            await _unitOfWork.AppsRepository.AddAsync(app);
            await _unitOfWork.CompleteAsync();

            return app;
        }

        public string GenerateJwt(AppDto appDto)
        {
            if (appDto == null) throw new NullReferenceException();

            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, $"{_configuration["Jwt:Subject"]}"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id", appDto.Id.ToString()),
                    new Claim("AppName", appDto.Name)
                   };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Issuer"], claims, expires: DateTime.UtcNow.AddYears(5), signingCredentials: signIn);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public Response<List<App>> GetAllApps(PaginationFilter filter)
        {
            var itemQuery = _unitOfWork.AppsRepository.GetAll().Where(c => c.IsActive);
            var items = itemQuery.Skip((filter.PageNumber - 1) * filter.PageSize)
                                        .Take(filter.PageSize)
                                        .ToList();
            var totalRecords = itemQuery.Count();

            return new Response<List<App>>(items, totalRecords);
        }

        public async Task<App> GetAppByIdAsync(Guid appId)
        {
            return await _unitOfWork.AppsRepository.SingleOrDefaultAsync(c => c.Id == appId);
        }

        public async Task<App> RemoveAppByIdAsync(Guid appId)
        {
            var app = await _unitOfWork.AppsRepository.SingleOrDefaultAsync(c => c.Id == appId);
            if (app == null) throw new NullReferenceException();
            if (!app.IsActive) throw new AlreadyDeletedException(); //this fails fast and prevents checking database.

            app.IsActive = false;
            await _unitOfWork.CompleteAsync();

            return app;
        }

        public async Task<App> UpdateAppAsync(AppDto appDto)
        {
            var app = await _unitOfWork.AppsRepository.SingleOrDefaultAsync(c => c.Id == appDto.Id && c.IsActive);
            if (app == null) throw new NullReferenceException();

            app.Id = appDto.Id;
            app.Name = appDto.Name;
            app.Players = appDto.Players;
            app.Messageable_players = appDto.Messageable_players;
            app.Updated_at = DateTime.Now;

            app.Gcm_key = appDto.Gcm_key;
            app.Chrome_web_origin = appDto.Chrome_web_origin;
            app.Chrome_web_default_notification_icon = appDto.Chrome_web_default_notification_icon;
            app.Chrome_web_sub_domain = appDto.Chrome_web_sub_domain;
            app.Apns_env = appDto.Apns_env;

            app.Apns_certificates = appDto.Apns_certificates;
            app.Safari_apns_certificate = appDto.Safari_apns_certificate;
            app.Safari_site_origin = appDto.Safari_site_origin;
            app.Safari_push_id = appDto.Safari_push_id;
            app.Site_name = appDto.Site_name;

            app.Safari_icon_16_16 = appDto.Safari_icon_16_16;
            app.Safari_icon_32_32 = appDto.Safari_icon_32_32;
            app.Safari_icon_64_64 = appDto.Safari_icon_64_64;
            app.Safari_icon_128_128 = appDto.Safari_icon_128_128;
            app.Safari_icon_256_256 = appDto.Safari_icon_256_256;
            
            app.Basic_auth_key = GenerateJwt(appDto);

            await _unitOfWork.CompleteAsync();
            return app;
        }
    }
}
