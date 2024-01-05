using Hyperbar.Lifecycles;
using Hyperbar.Templates;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hyperbar.Desktop
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddCommand<TCommandBuilder>(this IServiceCollection services, 
            string key)
            where TCommandBuilder :
            ICommandBuilder, new()
        {
            TCommandBuilder builder = new();
            IHost? host = new HostBuilder()
                .ConfigureServices(isolatedServices =>
                {
                    isolatedServices.AddTransient<ITemplateFactory, TemplateFactory>();
                    isolatedServices.AddTransient<ITemplateGeneratorFactory, TemplateGeneratorFactory>();

                    builder.Create(isolatedServices);
                }).Build();

            services.AddTransient<ICommandContext>(provider => new CommandContext(host.Services));
            return services;
        }
    }
}