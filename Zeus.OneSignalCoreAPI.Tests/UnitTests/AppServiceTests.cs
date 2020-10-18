using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Zeus.Core.Models;
using Zeus.Core.Requests;
using Zeus.Core.UnitOfWork;
using Zeus.EntityFrameworkCore;
using Zeus.OneSignalCoreAPI.Dto.Apps;
using Zeus.OneSignalCoreAPI.Services;
using Zeus.OneSignalCoreAPI.Tests.Extensions;
using Zeus.OneSignalCoreAPI.Tests.TestData;

namespace Zeus.OneSignalCoreAPI.Tests.UnitTests
{
    public class AppServiceTests : UnitTestBase
    {
        private readonly AppService _appService;
        private readonly List<App> _apps;
        private readonly App _app;

        public AppServiceTests()
        {
            _apps = new TestAppData().GetApps();
            _app = _apps.Where(c => c.Id.ToString() == "92911750-242d-4260-9e00-9d9034f139ce").FirstOrDefault();

            var unitOfWork = new Mock<IUnitOfWork>();
            var mapper = new Mock<IMapper>();
            var configuration = new Mock<IConfiguration>();
            var logger = new Mock<ILogger<AppService>>();

            unitOfWork.Setup(c => c.AppsRepository.GetAll()).Returns(_apps.GetQueryableMockDbSet());
            //unitOfWork.Setup(c => c.AppsRepository.FindAsync(It.IsAny<Expression<Func<App, bool>>>())).Returns(Task.FromResult(_app));
            unitOfWork.Setup(c => c.AppsRepository.FirstOrDefaultAsync(It.IsAny<Expression<Func<App, bool>>>())).Returns(Task.FromResult<App>(null));
            unitOfWork.Setup(c => c.AppsRepository.SingleOrDefaultAsync(It.IsAny<Expression<Func<App, bool>>>())).Returns(Task.FromResult(_app));

            configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Key")]).Returns("b0MjG9uMW0n2WswIlPLe3v3f6Wji2A");
            configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Issuer")]).Returns("OneSignalApi");
            configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Subject")]).Returns("OneSignalApi-AccessToken");

            _appService = new AppService(unitOfWork.Object, configuration.Object, logger.Object);
        }

        [Fact]
        public void GetAllCustomers()
        {
            var result = _appService.GetAllApps(new PaginationFilter(1, 10));

            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data.Count());
            Assert.Equal(2, result.TotalRecords);
            Assert.True(result.Succeeded);

            foreach (var app in result.Data)
            {
                var expected = _apps.Where(c => c.Id == app.Id).FirstOrDefault();
                Assert.Equal(expected, app);
            }
        }

        [Fact]
        public async void GetCustomerByIdAsync()
        {
            var result = await _appService.GetAppByIdAsync(new Guid("92911750-242d-4260-9e00-9d9034f139ce"));

            Assert.NotNull(result);
            Assert.Equal(_app, result);
        }

        [Fact]
        public async void RemoveCustomerByIdAsync()
        {
            var result = await _appService.RemoveAppByIdAsync(new Guid("92911750-242d-4260-9e00-9d9034f139ce"));

            Assert.NotNull(result);
            Assert.False(result.IsActive);
            Assert.Equal(_app, result);
        }

        [Fact]
        public async void UpdateCustomerAsync()
        {
            var appDto = new AppDto
            {
                Id = new Guid("92911750-242d-4260-9e00-9d9034f139ce"),
                Name = "Update Your app 1",
                Players = 150,
                Messageable_players = 143,
                Updated_at = DateTime.Now,
                Created_at = DateTime.Now,
                Gcm_key = "Update a gcm push key",
                Chrome_key = "Update A Chrome Web Push GCM key",
                Chrome_web_origin = "Update Chrome Web Push Site URL",
                Chrome_web_gcm_sender_id = "Update Chrome Web Push GCM Sender ID",
                Chrome_web_default_notification_icon = "http://yoursite.com/chrome_notification_icon",
                Chrome_web_sub_domain = "your_site_name",
                Apns_env = "sandbox",
                Apns_certificates = "Update Your apns certificate",
                Safari_apns_certificate = "Update Your Safari APNS certificate",
                Safari_site_origin = "Update The homename for your website for Safari Push, including http or https",
                Safari_push_id = "Update The certificate bundle ID for Safari Web Push",
                Safari_icon_16_16 = "http://onesignal.com/safari_packages/92911750-242d-4260-9e00-9d9034f139ce/16x16.png",
                Safari_icon_32_32 = "http://onesignal.com/safari_packages/92911750-242d-4260-9e00-9d9034f139ce/16x16@2.png",
                Safari_icon_64_64 = "http://onesignal.com/safari_packages/92911750-242d-4260-9e00-9d9034f139ce/32x32@2x.png",
                Safari_icon_128_128 = "http://onesignal.com/safari_packages/92911750-242d-4260-9e00-9d9034f139ce/128x128.png",
                Safari_icon_256_256 = "http://onesignal.com/safari_packages/92911750-242d-4260-9e00-9d9034f139ce/128x128@2x.png",
                Site_name = "Update The URL to your website for Web Push",
                Basic_auth_key = "Update NGEwMGZmMjItY2NkNy0xMWUzLTk5ZDUtMDAwYzI5NDBlNjJj",
                IsActive = true
            };

            _app.Id = appDto.Id;
            _app.Name = appDto.Name;
            _app.Players = appDto.Players;
            _app.Messageable_players = appDto.Messageable_players;
            _app.Updated_at = DateTime.Now;
            
            _app.Gcm_key = appDto.Gcm_key;
            _app.Chrome_web_origin = appDto.Chrome_web_origin;
            _app.Chrome_web_default_notification_icon = appDto.Chrome_web_default_notification_icon;
            _app.Chrome_web_sub_domain = appDto.Chrome_web_sub_domain;
            _app.Apns_env = appDto.Apns_env;
            
            _app.Apns_certificates = appDto.Apns_certificates;
            _app.Safari_apns_certificate = appDto.Safari_apns_certificate;
            _app.Safari_site_origin = appDto.Safari_site_origin;
            _app.Safari_push_id = appDto.Safari_push_id;
            _app.Site_name = appDto.Site_name;
            
            _app.Safari_icon_16_16 = appDto.Safari_icon_16_16;
            _app.Safari_icon_32_32 = appDto.Safari_icon_32_32;
            _app.Safari_icon_64_64 = appDto.Safari_icon_64_64;
            _app.Safari_icon_128_128 = appDto.Safari_icon_128_128;
            _app.Safari_icon_256_256 = appDto.Safari_icon_256_256;

            var result = await _appService.UpdateAppAsync(appDto);

            Assert.NotNull(result);
            Assert.Equal(_app, result);
        }

        [Fact]
        public async void CreateCustomerAsync()
        {
            var result = await _appService.CreateAppAsync(_app);

            Assert.NotNull(result);
            Assert.Equal(_app, result);
        }



    }
}
