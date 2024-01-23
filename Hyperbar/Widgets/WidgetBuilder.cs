using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace Hyperbar;

public class WidgetBuilder<TConfiguration>(TConfiguration configuration) :
    IWidgetBuilder<TConfiguration>
    where TConfiguration :
    WidgetConfiguration,
    new()
{
    private readonly IHostBuilder hostBuilder = new HostBuilder()
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
            services.AddHandler<WidgetConfigurationHandler>();

            services.AddSingleton<IValue<WidgetAvailability>, Value<WidgetAvailability>>(); 

            //services.AddConfiguration(configuration);
            services.AddConfiguration<WidgetConfiguration>(section: configuration.GetType().Name, 
                configuration: configuration);
        });

    public static IWidgetBuilder Configure(Action<TConfiguration> configurationDelegate)
    {
        TConfiguration configuration = new();
        configurationDelegate(configuration);

        return new WidgetBuilder<TConfiguration>(configuration);
    }

    public IWidgetHost Build()
    {
        IHost host = hostBuilder.Build();

        //if (host.Services.GetRequiredService<IConfigurationInitializer<TConfiguration>>()
        //    is IConfigurationInitializer<TConfiguration> configurationInitializer)
        //{
        //    configurationInitializer.InitializeAsync()
        //        .GetAwaiter()
        //        .GetResult();
        //}

        return (IWidgetHost)ActivatorUtilities.CreateInstance(host.Services,
            typeof(WidgetHost), host);
    }

    public IWidgetBuilder ConfigureServices(Action<IServiceCollection> configureDelegate)
    {
        hostBuilder.ConfigureServices(configureDelegate.Invoke);
        return this;
    }
}