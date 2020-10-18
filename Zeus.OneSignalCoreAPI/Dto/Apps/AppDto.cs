using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Zeus.OneSignalCoreAPI.Dto.Apps
{
    public class AppDto
    {
        [DisplayName("App Id")]
        public Guid Id { get; set; }

        [DisplayName("App Name")]
        public string Name { get; set; }

        [DisplayName("App Players")]
        public int Players { get; set; }

        [DisplayName("Messageable Players")]
        public int Messageable_players { get; set; }

        [DisplayName("Updated At")]
        public DateTime Updated_at { get; set; }

        [DisplayName("Created At")]
        public DateTime Created_at { get; set; }

        [DisplayName("GCM Key")]
        public string Gcm_key { get; set; }

        [DisplayName("Chrome Web Origin")]
        public string Chrome_web_origin { get; set; }

        [DisplayName("Chrome Key")]
        public string Chrome_key { get; set; }

        [DisplayName("Chrome Web Default Notification Icon")]
        public string Chrome_web_default_notification_icon { get; set; }

        [DisplayName("Chrome Web Sub Domain")]
        public string Chrome_web_sub_domain { get; set; }

        [DisplayName("Chrome Web GCM Sender Id")]
        public string Chrome_web_gcm_sender_id { get; set; }

        [DisplayName("APNS Environment")]
        public string Apns_env { get; set; }

        [DisplayName("APNS Certificates")]
        public string Apns_certificates { get; set; }

        [DisplayName("Safari APNS Certificate")]
        public string Safari_apns_certificate { get; set; }

        [DisplayName("Safari Site Origin")]
        public string Safari_site_origin { get; set; }

        [DisplayName("Safari Push ID")]
        public string Safari_push_id { get; set; }

        [DisplayName("Safari Icon 16x16")]
        public string Safari_icon_16_16 { get; set; }

        [DisplayName("Safari Icon 32x32")]
        public string Safari_icon_32_32 { get; set; }

        [DisplayName("Safari Icon 64x64")]
        public string Safari_icon_64_64 { get; set; }

        [DisplayName("Safari Icon 128x128")]
        public string Safari_icon_128_128 { get; set; }

        [DisplayName("Safari Icon 256x256")]
        public string Safari_icon_256_256 { get; set; }

        [DisplayName("Site Name")]
        public string Site_name { get; set; }

        [DisplayName("Basic Auth Key")]
        public string Basic_auth_key { get; set; }

        [DisplayName("Is Active")]
        public bool IsActive { get; set; }

        public override bool Equals(Object obj)
        {
            //Check for null and compare run - time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                AppDto app = (AppDto)obj;
                return (app.Id == this.Id &&
                        app.Name == this.Name &&
                        app.Players == this.Players &&
                        app.Messageable_players == this.Messageable_players &&
                        app.Updated_at == this.Updated_at &&
                        app.Created_at == this.Created_at &&

                        app.Gcm_key == this.Gcm_key &&
                        app.Chrome_web_origin == this.Chrome_web_origin &&
                        app.Chrome_web_default_notification_icon == this.Chrome_web_default_notification_icon &&
                        app.Chrome_web_sub_domain == this.Chrome_web_sub_domain &&
                        app.Apns_env == this.Apns_env &&

                        app.Apns_certificates == this.Apns_certificates &&
                        app.Safari_apns_certificate == this.Safari_apns_certificate &&
                        app.Safari_site_origin == this.Safari_site_origin &&
                        app.Safari_push_id == this.Safari_push_id &&
                        app.Site_name == this.Site_name &&

                        app.Safari_icon_16_16 == this.Safari_icon_16_16 &&
                        app.Safari_icon_32_32 == this.Safari_icon_32_32 &&
                        app.Safari_icon_64_64 == this.Safari_icon_64_64 &&
                        app.Safari_icon_128_128 == this.Safari_icon_128_128 &&
                        app.Safari_icon_256_256 == this.Safari_icon_256_256 &&
                        app.Basic_auth_key == this.Basic_auth_key);
            }
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
