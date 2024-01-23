using Hyperbar.Windows.Controls;
using Hyperbar.Windows.Interop;
using Hyperbar.Windows.UI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;
using System;
using System.Reflection;
using Windows.Media.Control;

namespace Hyperbar.Windows;

public partial class App :
    Application
{
    public App() => InitializeComponent();

    protected override async void OnLaunched(LaunchActivatedEventArgs args)
    {        
        base.OnLaunched(args);

        IHost? host = new HostBuilder()
            .UseContentRoot(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), 
                Assembly.GetEntryAssembly()?.GetName().Name!), true)
            .ConfigureAppConfiguration(config =>
            {
                config.AddJsonFile("Settings.json", true, true);
            })
            .ConfigureServices((context, services) =>
            {
                services.AddDefault();

                services.AddHostedService<AppService>();

                services.AddSingleton<IDispatcher, Dispatcher>();
                services.AddTransient<ITemplateFactory, TemplateFactory>();

                services.AddHandler<AppConfigurationChangedHandler>();
                services.AddConfiguration<AppConfiguration>(args =>
                {
                    args.Placement = DesktopBarPlacemenet.Top;
                });

                services.AddTransient<IInitializer, AppInitializer>();

                services.AddSingleton<DesktopBar>();
                services.AddContentTemplate<WidgetBarViewModel, WidgetBarView>();
                services.AddContentTemplate<WidgetContainerViewModel, WidgetContainerView>();

                services.AddTransient<IProxyServiceCollection<IWidgetBuilder>>(provider =>
                    new ProxyServiceCollection<IWidgetBuilder>(services =>
                    {
                        services.AddSingleton<IDispatcher, Dispatcher>();
                        services.AddTransient<ITemplateFactory, TemplateFactory>();
                        services.AddScoped<IVirtualKeyboard, VirtualKeyboard>();

                        services.AddHandler<KeyAcceleratorHandler>();
                        services.AddHandler<StartProcessHandler>();

                        services.AddTransient<IWidgetView, WidgetView>();

                        services.AddContentTemplate<WidgetButtonViewModel, WidgetButtonView>();
                        services.AddContentTemplate<WidgetSplitButtonViewModel, WidgetSplitButtonView>();
                    }));

                //services.AddTransient(provider =>
                //{
                //    static IEnumerable<WidgetContainerViewModel> Resolve(IServiceProvider services)
                //    {
                //        int index = 0;
                //        foreach (WidgetContext widgetContext in services.GetServices<WidgetContext>())
                //        {
                //            if (widgetContext.ServiceProvider.GetService<IWidget>() is IWidget widget)
                //            {
                //                if (widgetContext.ServiceProvider.GetServices<IWidgetViewModel>() is 
                //                    IEnumerable<IWidgetViewModel> viewModels)
                //                {
                //                    yield return (WidgetContainerViewModel)ActivatorUtilities.CreateInstance(widgetContext.ServiceProvider,
                //                        typeof(WidgetContainerViewModel), viewModels, index % 2 == 1, widget.Id);

                //                    index++;
                //                }
                //            }
                //        }
                //    }

                //    return Resolve(provider);
                //});
            })
        .Build();

        var d = host.Services.GetService<JsonConfigurationProvider>();

        await host.RunAsync();
    }
}