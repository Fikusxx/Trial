

namespace Trial.WebAPI.Caching;

public static class CachingOptions
{
    public static IServiceCollection AddCaching(this IServiceCollection services)
    {
        services.AddHttpCacheHeaders(
            expirationOptions =>
        {
            expirationOptions.MaxAge = 60;
            expirationOptions.CacheLocation = Marvin.Cache.Headers.CacheLocation.Private;
        },
        validationOptions =>
        {
            validationOptions.MustRevalidate = true;
        });

        services.AddResponseCaching();

        return services;
    }
}
