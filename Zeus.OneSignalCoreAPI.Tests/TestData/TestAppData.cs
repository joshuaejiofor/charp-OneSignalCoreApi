using System;
using System.Collections.Generic;
using System.Text;
using Zeus.Core.Models;

namespace Zeus.OneSignalCoreAPI.Tests.TestData
{
    public class TestAppData
    {
        public List<App> GetApps()
        {
            return new List<App>
            {
                new App{
                Id = new Guid("92911750-242d-4260-9e00-9d9034f139ce"),
                Name = "Your app 1",
                Players = 150,
                Messageable_players = 143,
                Updated_at = DateTime.Now,
                Created_at = DateTime.Now,
                Gcm_key = "a gcm push key",
                Chrome_key = "A Chrome Web Push GCM key",
                Chrome_web_origin = "Chrome Web Push Site URL",
                Chrome_web_gcm_sender_id = "Chrome Web Push GCM Sender ID",
                Chrome_web_default_notification_icon = "http://yoursite.com/chrome_notification_icon",
                Chrome_web_sub_domain = "your_site_name",
                Apns_env = "sandbox",
                Apns_certificates = "Your apns certificate",
                Safari_apns_certificate = "Your Safari APNS certificate",
                Safari_site_origin = "The homename for your website for Safari Push, including http or https",
                Safari_push_id = "The certificate bundle ID for Safari Web Push",
                Safari_icon_16_16 = "http://onesignal.com/safari_packages/92911750-242d-4260-9e00-9d9034f139ce/16x16.png",
                Safari_icon_32_32 = "http://onesignal.com/safari_packages/92911750-242d-4260-9e00-9d9034f139ce/16x16@2.png",
                Safari_icon_64_64 = "http://onesignal.com/safari_packages/92911750-242d-4260-9e00-9d9034f139ce/32x32@2x.png",
                Safari_icon_128_128 = "http://onesignal.com/safari_packages/92911750-242d-4260-9e00-9d9034f139ce/128x128.png",
                Safari_icon_256_256 = "http://onesignal.com/safari_packages/92911750-242d-4260-9e00-9d9034f139ce/128x128@2x.png",
                Site_name = "The URL to your website for Web Push",
                Basic_auth_key = "NGEwMGZmMjItY2NkNy0xMWUzLTk5ZDUtMDAwYzI5NDBlNjJj",
                IsActive = true
              },
              new App{
                Id = new Guid("e4e87830-b954-11e3-811d-f3b376925f15"),
                Name = "Your app 2",
                Players = 100,
                Messageable_players = 80,
                Updated_at = DateTime.Now,
                Created_at = DateTime.Now,
                Gcm_key = "a gcm push key",
                Chrome_key = "A Chrome Web Push GCM key",
                Chrome_web_origin = "Chrome Web Push Site URL",
                Chrome_web_gcm_sender_id = "Chrome Web Push GCM Sender ID",
                Chrome_web_default_notification_icon = "http://yoursite.com/chrome_notification_icon",
                Chrome_web_sub_domain = "your_site_name",
                Apns_env = "sandbox",
                Apns_certificates = "Your apns certificate",
                Safari_apns_certificate = "Your Safari APNS certificate",
                Safari_site_origin = "The homename for your website for Safari Push, including http or https",
                Safari_push_id = "The certificate bundle ID for Safari Web Push",
                Safari_icon_16_16 = "http://onesignal.com/safari_packages/e4e87830-b954-11e3-811d-f3b376925f15/16x16.png",
                Safari_icon_32_32 = "http://onesignal.com/safari_packages/e4e87830-b954-11e3-811d-f3b376925f15/16x16@2.png",
                Safari_icon_64_64 = "http://onesignal.com/safari_packages/e4e87830-b954-11e3-811d-f3b376925f15/32x32@2x.png",
                Safari_icon_128_128 = "http://onesignal.com/safari_packages/e4e87830-b954-11e3-811d-f3b376925f15/128x128.png",
                Safari_icon_256_256 = "http://onesignal.com/safari_packages/e4e87830-b954-11e3-811d-f3b376925f15/128x128@2x.png",
                Site_name = "The URL to your website for Web Push",
                Basic_auth_key = "NGEwMGZmMjItY2NkNy0xMWUzLTk5ZDUtMDAwYzI5NDBlNjJj",
                IsActive = true
              }
            };
        }
    }
}
