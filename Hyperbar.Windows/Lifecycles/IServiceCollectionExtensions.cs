using Hyperbar.Lifecycles;
using Hyperbar.Templates;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hyperbar.Windows
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddWidget<TCommandBuilder>(this IServiceCollection services, 
            string key)
            where TCommandBuilder :
            IWidgetBuilder, new()
        {
            TCommandBuilder builder = new();
            IHost? host = new HostBuilder()
                .ConfigureServices(isolatedServices =>
                {
                    isolatedServices.AddTransient<ITemplateFactory, TemplateFactory>();
                    isolatedServices.AddTransient<ITemplateGeneratorFactory, TemplateGeneratorFactory>();

                    builder.Create(isolatedServices);
                }).Build();

            services.AddTransient<IWidgetContext>(provider => new WidgetContext(host.Services));
            return services;
        }
    }
}