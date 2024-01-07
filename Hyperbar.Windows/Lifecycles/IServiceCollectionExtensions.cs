using Hyperbar.Windows.Interop;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hyperbar.Windows
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddWidgetProvider<TWidgetProvider>(this IServiceCollection services)
            where TWidgetProvider :
            IWidgetProvider, new()
        {
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
                    isolatedServices.AddHostedService<WidgetService>();

                    isolatedServices.AddSingleton<IServiceFactory>(provider =>
                        new ServiceFactory((type, parameters) => ActivatorUtilities.CreateInstance(provider, type, parameters!)));
                    
                    isolatedServices.AddSingleton<IVirtualKeyboard, VirtualKeyboard>();

                    isolatedServices.AddSingleton<IMediator, Mediator>();
                    isolatedServices.AddHandler<KeyAcceleratorHandler>();

                    isolatedServices.AddTransient<IWidgetView, WidgetView>();
                    isolatedServices.AddContentTemplate<WidgetButtonViewModel, WidgetButtonView>();

                    isolatedServices.AddTransient<ITemplateFactory, TemplateFactory>();
                    isolatedServices.AddTransient<ITemplateGeneratorFactory, TemplateGeneratorFactory>();

                    builder.Create(context, isolatedServices);

                }).Build();

            services.AddTransient<IWidgetContext>(provider => new WidgetContext(host.Services));

            host.Start();
            return services;
        }
    }
}