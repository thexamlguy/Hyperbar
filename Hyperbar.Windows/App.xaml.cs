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

                services.AddSingleton((IDispatcher)new Dispatcher(DispatcherQueue.GetForCurrentThread()));

                services.AddTransient<INavigationProvider, NavigationProvider>();
                services.AddSingleton<NavigationTargetCollection>();
                services.AddTransient<INavigationTargetProvider, NavigationTargetProvider>();

                services.AddTransient<IViewModelContentBinder, ViewModelContentBinder>();

                services.AddTransient<IViewModelTemplateProvider, ViewModelTemplateProvider>();
                services.AddTransient<IViewModelTemplateSelector, ViewModelTemplateSelector>();

                services.AddHandler<AppConfigurationChangedHandler>();
                services.AddConfiguration((AppConfiguration args) =>
                {
                    args.Placement = DesktopApplicationBarPlacemenet.Top;
                });

                services.AddNavigationHandler<WindowHandler>();
                services.AddNavigationHandler<ContentControlHandler>();

                services.AddSingleton<DesktopApplicationBar>();
                services.AddContentTemplate<ApplicationBarViewModel, ApplicationBarView>();
                services.AddContentTemplate<PrimaryViewModel, PrimaryView>();
                services.AddContentTemplate<SecondaryViewModel, SecondaryView>();

                services.AddContentTemplate<SettingsButtonViewModel, SettingsButtonView>();
                services.AddContentTemplate<SettingsViewModel, SettingsView>("Settings");

                services.AddContentTemplate<GeneralSettingsNavigationViewModel, GeneralSettingsNavigationView>();
                services.AddContentTemplate<WidgetSettingsNavigationViewModel, WidgetSettingsNavigationView>();
                services.AddContentTemplate<WidgetNavigationViewModel, WidgetNavigationView>();

                services.AddContentTemplate<WidgetSettingsViewModel, WidgetSettingsView>("WidgetSettings");

                services.AddHandler<WidgetNavigationViewModelEnumerator>();
                services.AddTransient<IInitializer, AppInitializer>();
            })
        .Build();

        await host.RunAsync();
    }
}