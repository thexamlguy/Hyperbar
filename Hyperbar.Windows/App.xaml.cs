﻿using Hyperbar.Widget.Contextual;
using Hyperbar.Windows.Controls;
using Hyperbar.Windows.Primary;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using System.Text.Json;

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
                services.AddHostedService<AppService>();

                services.AddTransient<IInitializer, AppInitializer>();
                services.AddTransient<ITemplateFactory, TemplateFactory>();

                services.AddTransient<DesktopFlyout>();
                services.AddContentTemplate<CommandViewModel, CommandView>();

             //   services.AddWidgetProvider<ContextualWidgetProvider>();
                services.AddWidgetProvider<PrimaryWidgetProvider>();

                services.AddTransient(provider =>
                {
                    static IEnumerable<IWidgetViewModel> Resolve(IServiceProvider services)
                    {
                        foreach (IWidgetContext widgetContext in services.GetServices<IWidgetContext>())
                        {
                            if (widgetContext.ServiceProvider.GetService<IWidgetViewModel>() is
                                IWidgetViewModel viewModel)
                            {
                                yield return viewModel;
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