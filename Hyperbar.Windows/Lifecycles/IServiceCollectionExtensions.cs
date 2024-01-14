using Hyperbar.Windows.Interop;
using Hyperbar.Windows.UI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Dispatching;

namespace Hyperbar.Windows
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddWidgetProvider<TWidgetProvider>(this IServiceCollection services)
            where TWidgetProvider :
            IWidgetProvider, new()
        {
            DispatcherQueueSynchronizationContext context = new(DispatcherQueue.GetForCurrentThread());
            SynchronizationContext.SetSynchronizationContext(context);

            TWidgetProvider builder = new();
            IHost? host = new HostBuilder()
                .UseContentRoot(AppContext.BaseDirectory)
                .ConfigureAppConfiguration(config =>
                {
                    config.SetBasePath(AppContext.BaseDirectory);
                    config.AddJsonFile("Settings.json", true, true);

                    config.Build();
                })
                .ConfigureServices((context, isolatedServices) =>
                {
                    isolatedServices.AddScoped<IServiceFactory>(provider =>
                        new ServiceFactory((type, parameters) => ActivatorUtilities.CreateInstance(provider, type, parameters!)));

                    isolatedServices.AddHostedService<WidgetService>();

                    isolatedServices.AddTransient<ITemplateFactory, TemplateFactory>();
                    isolatedServices.AddScoped<IMediator, Mediator>();
                    isolatedServices.AddScoped<IDisposer, Disposer>();

                    isolatedServices.AddScoped<IVirtualKeyboard, VirtualKeyboard>();

                    isolatedServices.AddHandler<KeyAcceleratorHandler>();
                    isolatedServices.AddHandler<StartProcessHandler>();

                    isolatedServices.AddTransient<IWidgetView, WidgetView>();

                    isolatedServices.AddContentTemplate<WidgetContainerViewModel, WidgetContainerView>();
                    isolatedServices.AddContentTemplate<WidgetButtonViewModel, WidgetButtonView>();
                    isolatedServices.AddContentTemplate<WidgetSplitButtonViewModel, WidgetSplitButtonView>();

                    builder.Create(context, isolatedServices);

                }).Build();

            services.AddTransient<IWidgetContext>(provider => new WidgetContext(host.Services));

            host.Start();
            return services;
        }
    }
}