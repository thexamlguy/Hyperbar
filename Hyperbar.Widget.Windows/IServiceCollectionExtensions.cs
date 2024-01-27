using Hyperbar.Interop.Windows;
using Hyperbar.UI.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.Widget.Windows;

public static class IServiceCollectionExtensions
{ 
    public static IServiceCollection AddWidgetWindows(this IServiceCollection services)
    {
        services.AddContentTemplate<WidgetBarViewModel, WidgetBarView>();

        // We need to feed information to the Widgets about our Windows host,
        // so the Windows host can make discussions how to display and interact with the widgets. 

        services.AddTransient<IProxyServiceCollection<IWidgetBuilder>>(provider =>
            new ProxyServiceCollection<IWidgetBuilder>(services =>
            {
                services.AddSingleton(provider.GetRequiredService<IDispatcher>());
                services.AddTransient<IFactory<IWidgetHost, WidgetContainerViewModel?>,
                    WidgetContainerFactory>();

                services.AddTransient<ITemplateFactory, TemplateFactory>();

                services.AddScoped<IVirtualKeyboard, VirtualKeyboard>();
                services.AddHandler<KeyAcceleratorHandler>();
                services.AddHandler<StartProcessHandler>();

                services.AddHandler<WidgetViewModelEnumerator>();

                services.AddTransient<IWidgetView, WidgetView>();
                services.AddTransient<IInitializer, WidgetResourceInitialization>();

                services.AddContentTemplate<WidgetContainerViewModel, WidgetContainerView>();
                services.AddContentTemplate<WidgetButtonViewModel, WidgetButtonView>();
                services.AddContentTemplate<WidgetSplitButtonViewModel, WidgetSplitButtonView>();
            }));

        return services;
    }
}