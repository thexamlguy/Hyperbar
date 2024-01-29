using Hyperbar.Controls.Windows;
using Hyperbar.UI.Windows;
using Hyperbar.Widget;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using System.Reflection;
using Hyperbar.Widget.Windows;
using Microsoft.UI.Xaml.Markup;

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
                services.AddWidget();
                services.AddWidgetWindows();
                services.AddXamlMetadataProvider();

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
            })
        .Build();

        await host.RunAsync();
    }
}