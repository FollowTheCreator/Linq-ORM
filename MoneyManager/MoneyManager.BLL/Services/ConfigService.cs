using Microsoft.Extensions.Configuration;
using MoneyManager.BLL.Interfaces.Services;
using System;

namespace MoneyManager.BLL.Services
{
    public class ConfigService : IConfigService
    {
        private readonly IConfiguration _config;

        private const string DataSeedingSection = "DataSeeding";
        private const string PageInfoSection = "PageInfo";

        public ConfigService(IConfiguration config)
        {
            _config = config;
        }

        public int GetPageSize()
        {
            return GetValue<int>($"{PageInfoSection}:PageSize");
        }

        public int GetCountOfUsers()
        {
            return GetValue<int>($"{DataSeedingSection}:CountOfUsers");
        }

        public int GetCountOfAssets()
        {
            return GetValue<int>($"{DataSeedingSection}:CountOfAssets");
        }

        public int GetCountOfCategories()
        {
            return GetValue<int>($"{DataSeedingSection}:CountOfCategories");
        }

        public int GetCountOfTransactions()
        {
            return GetValue<int>($"{DataSeedingSection}:CountOfTransactions");
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
