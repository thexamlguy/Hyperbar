using CustomExtensions.WinUI;
using Hyperbar.Controls.Windows;
using Hyperbar.Interop.Windows;
using Hyperbar.UI.Windows;
using Hyperbar.Widget;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using System.Reflection;

namespace Hyperbar.Windows;

public partial class App :
    Application
{
    public App()
    {
        InitializeComponent();
        ApplicationExtensionHost.Initialize(this);
    }

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
                services.AddWidget();
                services.AddHostedService<AppService>();

                services.AddSingleton<IDispatcher>(new Dispatcher(DispatcherQueue.GetForCurrentThread()));
                services.AddTransient<ITemplateFactory, TemplateFactory>();

                services.AddHandler<AppConfigurationChangedHandler>();
                services.AddConfiguration<AppConfiguration>(args =>
                {
                    args.Placement = DesktopBarPlacemenet.Top;
                });

                services.AddTransient<IInitializer, AppInitializer>();

                services.AddSingleton<DesktopBar>();
                services.AddContentTemplate<WidgetBarViewModel, WidgetBarView>();

                services.AddTransient<IProxyServiceCollection<IWidgetBuilder>>(provider =>
                    new ProxyServiceCollection<IWidgetBuilder>(services =>
                    {
                        services.AddSingleton<IDispatcher>(new Dispatcher(DispatcherQueue.GetForCurrentThread()));

                        services.AddTransient<IFactory<IWidgetHost, WidgetContainerViewModel?>, 
                            WidgetContainerFactory>();

                        services.AddTransient<ITemplateFactory, TemplateFactory>();

                        services.AddScoped<IVirtualKeyboard, VirtualKeyboard>();
                        services.AddHandler<KeyAcceleratorHandler>();
                        services.AddHandler<StartProcessHandler>();

                        services.AddHandler<WidgetViewModelEnumerator>();

                        services.AddTransient<IWidgetView, WidgetView>();

                        services.AddContentTemplate<WidgetContainerViewModel, WidgetContainerView>();
                        services.AddContentTemplate<WidgetButtonViewModel, WidgetButtonView>();
                        services.AddContentTemplate<WidgetSplitButtonViewModel, WidgetSplitButtonView>();
                    }));
            })
        .Build();

        await host.RunAsync();
    }
}