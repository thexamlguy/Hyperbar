using Hyperbar.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hyperbar.Windows
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddWidget<TCommandBuilder>(this IServiceCollection services)
            where TCommandBuilder :
            IWidgetBuilder, new()
        {
            TCommandBuilder builder = new();
            IHost? host = new HostBuilder()
                .ConfigureServices(isolatedServices =>
                {
                    isolatedServices.AddSingleton<IServiceFactory>(provider =>
                        new ServiceFactory((type, parameters) => ActivatorUtilities.CreateInstance(provider, type, parameters!)));

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