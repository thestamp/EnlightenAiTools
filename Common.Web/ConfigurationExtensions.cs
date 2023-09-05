using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Web;

//Jeffrey Lefebvre, https://github.com/thestamp/EnlightenTools
public static class ConfigurationExtensions
{
    public static T BindAndAddSingleton<T>(this IConfiguration configuration, IServiceCollection services, string sectionName) where T : class, new()
    {
        var settings = new T();
        configuration.GetSection(sectionName).Bind(settings);
        services.AddSingleton(settings);
        return settings;
    }
}