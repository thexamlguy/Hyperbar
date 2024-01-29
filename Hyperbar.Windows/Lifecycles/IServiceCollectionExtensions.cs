using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Markup;
using System.Reflection;

namespace Hyperbar.Widget;

public static class IServiceCollectionExtensions
{ 
    public static IServiceCollection AddXamlMetadataProvider(this IServiceCollection services)
    {
        object? appProvider = Application.Current.GetType().GetProperty("_AppProvider", BindingFlags.NonPublic | BindingFlags.Instance)?
                .GetValue(Application.Current);

        object? provider = appProvider?.GetType().GetProperty("Provider", BindingFlags.NonPublic | BindingFlags.Instance)?
            .GetValue(appProvider);

        PropertyInfo? othersProviderProperty = provider?.GetType().GetProperty("OtherProviders", BindingFlags.NonPublic | BindingFlags.Instance,
            null, typeof(List<IXamlMetadataProvider>), [], null);

        List<IXamlMetadataProvider> xamlMetadataProviders  = othersProviderProperty?.GetValue(provider) as List<IXamlMetadataProvider> ?? [];

        services.AddSingleton<IList<IXamlMetadataProvider>>(xamlMetadataProviders);
        return services;
    }
}