using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace Hyperbar;

public class WidgetBuilder : 
    IWidgetBuilder,
    IWidgetServiceBuilder
{
    private readonly IHostBuilder hostBuilder;

    public WidgetBuilder(IWidgetConfiguration configuration)
    {
        hostBuilder = new HostBuilder()
            .UseContentRoot(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                Assembly.GetEntryAssembly()?.GetName().Name!), true)
            .ConfigureAppConfiguration(config =>
            {
                config.AddJsonFile("Settings.json", true, true);
            })
            .ConfigureServices((context, services) =>
            {
                services.AddScoped<IServiceFactory>(provider =>
                    new ServiceFactory((type, parameters) =>
                        ActivatorUtilities.CreateInstance(provider, type, parameters!)));

                services.AddHostedService<WidgetService>();

                services.AddScoped<IMediator, Mediator>();
                services.AddScoped<IDisposer, Disposer>();
                services.AddConfiguration<IWidgetConfiguration>(configuration);
            });
    }

    public static IWidgetBuilder Configure<TWidgetConfiguration>(Action<TWidgetConfiguration> configurationDelegate)
        where TWidgetConfiguration :
        IWidgetConfiguration,
        new()
    {
        TWidgetConfiguration configuration = new();
        configurationDelegate(configuration);

        return new WidgetBuilder(configuration);
    }

    public IWidgetHost Build()
    {
        IHost host = hostBuilder.Build();
        return new WidgetHost(host);
    }

    public IWidgetBuilder ConfigureServices(Action<IServiceCollection> configureDelegate)
    {
        hostBuilder.ConfigureServices(configureDelegate.Invoke);
        return this;
    }

    public void ConfigureWidgetServices(IWidgetServiceCollection widgetServices) =>
        hostBuilder.ConfigureServices(services => services.AddRange(widgetServices.Services));
}