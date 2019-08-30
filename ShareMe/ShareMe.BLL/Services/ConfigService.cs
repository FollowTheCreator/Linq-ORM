using Microsoft.Extensions.Configuration;
using ShareMe.BLL.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShareMe.BLL.Services
{
    public class ConfigService : IConfigService
    {
        private readonly IConfiguration _config;

        private const string PageInfoSection = "PageInfo";

        public ConfigService(IConfiguration config)
        {
            _config = config;
        }

        public int GetPageSize()
        {
            return GetValue<int>($"{PageInfoSection}:PageSize");
        }

        public int GetPopularPostsCount()
        {
            return GetValue<int>($"{PageInfoSection}:PopularPostsCount");
        }

        private T GetValue<T>(string path)
        {
            try
            {
                var value = _config.GetSection(path).Value;

                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"can not get value from config {path}", e);
            }
        }
    }
}
