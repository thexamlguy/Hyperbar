using Hyperbar.Extensions;
using Hyperbar.Windows.Win32;
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
                .ConfigureServices(isolatedServices =>
                {
                    isolatedServices.AddSingleton<IServiceFactory>(provider =>
                        new ServiceFactory((type, parameters) => ActivatorUtilities.CreateInstance(provider, type, parameters!)));
                    
                    isolatedServices.AddSingleton<IVirtualKeyboard, VirtualKeyboard>();

                    isolatedServices.AddSingleton<IMediator, Mediator>();
                    isolatedServices.AddHandler<KeyAcceleratorCommandHandler>();

                    isolatedServices.AddTransient<IWidgetView, WidgetView>();
                    isolatedServices.AddContentTemplate<WidgetButtonViewModel, WidgetButtonView>();

                    isolatedServices.AddTransient<ITemplateFactory, TemplateFactory>();
                    isolatedServices.AddTransient<ITemplateGeneratorFactory, TemplateGeneratorFactory>();

                    builder.Create(isolatedServices);
                }).Build();

            services.AddTransient<IWidgetContext>(provider => new WidgetContext(host.Services));
            return services;
        }
    }
}