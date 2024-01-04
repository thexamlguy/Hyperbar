using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hyperbar.Desktop
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddSomething<TCommandBuilder>(this IServiceCollection services)
            where TCommandBuilder :
            ICommandBuilder, new()
        {
            TCommandBuilder builder = new();
            IHost? host = new HostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddTransient<ITemplateFactory, TemplateFactory>();
                    services.AddTransient<ITemplateGeneratorFactory, TemplateGeneratorFactory>();

                    builder.Create(services);
                }).Build();

            services.AddTransient<ICommandContext>(provider => new CommandContext(host.Services));
            return services;
        }
    }
}