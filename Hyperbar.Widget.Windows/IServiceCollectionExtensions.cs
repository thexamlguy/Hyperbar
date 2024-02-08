using Hyperbar.Interop.Windows;
using Hyperbar.UI.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Markup;
using System.Collections.Generic;

namespace Hyperbar.Widget.Windows;

public static class IServiceCollectionExtensions
{ 
    public static IServiceCollection AddWidgetWindows(this IServiceCollection services)
    {
        // We need to feed information to the Widgets about our Windows host,
        // so the Windows host can make discussions how to display and interact with the widgets. 

        services.AddTransient<IProxyServiceCollection<IWidgetBuilder>>(provider =>
            new ProxyServiceCollection<IWidgetBuilder>(services =>
            {
                services.AddSingleton(provider.GetRequiredService<IList<IXamlMetadataProvider>>());
                services.AddSingleton(provider.GetRequiredService<IDispatcher>());

                services.AddTransient<IViewModelTemplateDescriptorProvider, ViewModelTemplateDescriptorProvider>();
                services.AddTransient<IViewModelTemplateFactory, ViewModelTemplateFactory>();

                services.AddScoped<IVirtualKeyboard, VirtualKeyboard>();
                services.AddHandler<KeyAcceleratorHandler>();
                services.AddHandler<StartProcessHandler>();

                services.AddHandler<WidgetViewModelEnumerator>();

                services.AddTransient<IWidgetView, WidgetView>();
                services.AddTransient<IInitializer, WidgetResourceInitializer>();
                services.AddTransient<IInitializer, WidgetXamlMetadataInitializer>();

                services.AddContentTemplate<WidgetButtonViewModel, WidgetButtonView>();
                services.AddContentTemplate<WidgetSplitButtonViewModel, WidgetSplitButtonView>();
            }));

        return services;
    }
}