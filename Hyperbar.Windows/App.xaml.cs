using Hyperbar.Windows.Controls;
using Hyperbar.Windows.Primary;
using Hyperbar.Windows.UI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;

namespace Hyperbar.Windows;

public partial class App :
    Application
{
    public App() => InitializeComponent();

    protected override async void OnLaunched(LaunchActivatedEventArgs args)
    {        
        base.OnLaunched(args);

        DispatcherQueueSynchronizationContext context = new(DispatcherQueue.GetForCurrentThread());
        SynchronizationContext.SetSynchronizationContext(context);

        IHost? host = Host.CreateDefaultBuilder()
            .UseContentRoot(AppContext.BaseDirectory)
            .ConfigureAppConfiguration(config =>
            {
                config.SetBasePath(AppContext.BaseDirectory);
                config.AddJsonFile("Settings.json", true, true);

                config.Build();
            })
            .ConfigureServices((context, services) =>
            {
                services.AddSingleton<IServiceFactory>(provider =>
                    new ServiceFactory((type, parameters) => ActivatorUtilities.CreateInstance(provider, type, parameters!)));

                services.AddSingleton<IMediator, Mediator>();
                services.AddSingleton<IDisposer, Disposer>();

                services.AddHostedService<AppService>();

                services.AddTransient<IInitializer, AppInitializer>();
                services.AddTransient<ITemplateFactory, TemplateFactory>();

                services.AddTransient<DesktopFlyout>();

                services.AddContentTemplate<WidgetBarViewModel, WidgetBarView>();

                //services.AddWidgetProvider<MediaControllerWidgetProvider>();
                services.AddWidgetProvider<PrimaryWidgetProvider>();

                services.AddTransient(provider =>
                {
                    static IEnumerable<WidgetContainerViewModel> Resolve(IServiceProvider services)
                    {
                        int index = 0;
                        foreach (IWidgetContext widgetContext in services.GetServices<IWidgetContext>())
                        {
                            if (widgetContext.ServiceProvider.GetServices<IWidgetViewModel>() is
                                IEnumerable<IWidgetViewModel> viewModels)
                            {
                                yield return (WidgetContainerViewModel)ActivatorUtilities.CreateInstance(widgetContext.ServiceProvider,
                                    typeof(WidgetContainerViewModel), viewModels, index % 2 == 1);

                                index++;
                            }
                        }
                    }

                    return Resolve(provider);
                });
            })
            .Build();

        await host.RunAsync();
    }
}