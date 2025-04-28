using Kroki;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection;

public static class KrokiExtensions
{
    public static IServiceCollection AddKrokiClient(this IServiceCollection services, Action<KrokiClientOptions>? configureOptions = null)
    {
        var optionsBuilder = services.AddOptions<KrokiClientOptions>();
        if(configureOptions is not null)
        {
            optionsBuilder.Configure(configureOptions);
        }
        services.AddSingleton(services =>
        {
            var options = services.GetRequiredService<IOptions<KrokiClientOptions>>().Value;
            return new KrokiClient(options.Endpoint);
        });
        return services;
    }
}