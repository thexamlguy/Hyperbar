using Hyperbar.Lifecycles;
using Hyperbar.Templates;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hyperbar.Windows
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddCommand<TCommandBuilder>(this IServiceCollection services, 
            string key)
            where TCommandBuilder :
            ICommandWidgetBuilder, new()
        {
            TCommandBuilder builder = new();
            IHost? host = new HostBuilder()
                .ConfigureServices(isolatedServices =>
                {
                    isolatedServices.AddTransient<ITemplateFactory, TemplateFactory>();
                    isolatedServices.AddTransient<ITemplateGeneratorFactory, TemplateGeneratorFactory>();

                    builder.Create(isolatedServices);
                }).Build();

            services.AddTransient<ICommandWidgetContext>(provider => new CommandWidgetContext(host.Services));
            return services;
        }
    }
}