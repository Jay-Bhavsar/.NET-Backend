using Microsoft.Extensions.Configuration;

namespace GeeksConfiguration
{
    public interface IConfigManager
    {
        string TokenKey { get; }
    }
}