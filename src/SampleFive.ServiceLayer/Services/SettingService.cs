using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SampleFive.PresentaionLayer;
using SampleFive.ServiceLayer.Interfaces;

namespace SampleFive.ServiceLayer.Services
{
    public class SettingService : ISettingService
    {
        private readonly IConfigurationRoot _configurationRoot;
        private readonly IOptions<SmtpConfig> _settings;

        public SettingService(IConfigurationRoot configurationRoot, IOptions<SmtpConfig> settings)
        {
            _configurationRoot = configurationRoot;
            _settings = settings;
        }

        public SiteCopyRightConfig GetSiteCopyRightConfig()
        {
            return new SiteCopyRightConfig
            {
                Title = "CopyRight Test SampleFive",
                Url = "http://bookkhan.com/"
            };
        }

        public string GetSiteName()
        {
            return "I'm ready for service to my King!";
        }
        public string GetSiteName2()
        {
            var key1 = _configurationRoot["Key1"];
            var val = _configurationRoot.GetValue<int>("key-name", defaultValue: 10);
            var user1 = _configurationRoot["Auth:Users:0"];
            var authUsers = _configurationRoot.GetSection("Auth:Users").GetChildren().Select(x => x.Value).ToArray();
            var loggingIncludeScopes = _configurationRoot["Logging:IncludeScopes"];
            var loggingLoggingLogLevelDefault = _configurationRoot["Logging:LogLevel:Default"];

            return $"Command of King : {key1}";
        }

        public string GetSiteName3()
        {
            var key1 = _configurationRoot["Key1"];
            var server = _settings.Value.Server;

            return $"Command of King : {key1} - {server}";
        }

        public string GetSiteWelcome(string title="my Kingdom")
        {
            return $"Welcome to {title}";
        }
    }
}