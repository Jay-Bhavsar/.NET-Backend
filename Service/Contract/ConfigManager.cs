using Microsoft.Extensions.Configuration;

namespace GeeksConfiguration
{
    public class ConfigManager : IConfigManager
    {
        private readonly IConfiguration _configuration;
        public ConfigManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string TokenKey
        {
            get
            {
                return _configuration["Jwt:Key"];
            }
        }
    }
}