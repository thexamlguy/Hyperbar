using Hyperbar.Interop.Windows;
using Hyperbar.UI.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Markup;

namespace Hyperbar.Widget.Windows;

public static class IServiceCollectionExtensions
{ 
    public static IServiceCollection AddWidgetWindows(this IServiceCollection services)
    {
        services.AddTransient((Func<IServiceProvider, IProxyServiceCollection<IWidgetBuilder>>)(provider =>
            new ProxyServiceCollection<IWidgetBuilder>(services =>
            {
                services.AddSingleton(provider.GetRequiredService<IList<IXamlMetadataProvider>>());
                services.AddSingleton(provider.GetRequiredService<IDispatcher>());
                
                services.AddTransient<IViewModelTemplateProvider, ViewModelTemplateProvider>();
                services.AddTransient<IViewModelTemplateSelector, ViewModelTemplateSelector>();

                services.AddScoped<IVirtualKeyboard, VirtualKeyboard>();
                services.AddHandler<KeyAcceleratorHandler>();
                services.AddHandler<StartProcessHandler>();

                services.AddTransient<IViewModelContentBinder, ViewModelContentBinder>();

                services.AddNavigationHandler<WindowHandler>();
                services.AddNavigationHandler<ContentControlHandler>();

                services.AddHandler<WidgetViewModelEnumerator>();

                services.AddTransient<IWidgetView, WidgetView>();
                services.AddTransient<IInitializer, WidgetResourceInitializer>();
                services.AddTransient<IInitializer, WidgetXamlMetadataInitializer>();

                services.AddContentTemplate<WidgetButtonViewModel, WidgetButtonView>();
                services.AddContentTemplate<WidgetSplitButtonViewModel, WidgetSplitButtonView>();

                services.AddContentTemplate<WidgetSettingsNavigationViewModel, WidgetSettingsNavigationView>();
                services.AddContentTemplate<WidgetSettingsViewModel, WidgetSettingsView>("WidgetSettings");
            })));

        return services;
    }
}