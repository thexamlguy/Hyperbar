using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace Hyperbar.Widget;

public class WidgetBuilder : 
    IWidgetBuilder
{
    private readonly IHostBuilder hostBuilder;

    private WidgetBuilder()
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
                services.AddSingleton<IWidgetHost, WidgetHost>();
                services.AddScoped<IServiceFactory>(provider =>
                    new ServiceFactory((type, parameters) =>
                        ActivatorUtilities.CreateInstance(provider, type, parameters!)));
                services.AddScoped<IMediator, Mediator>();
                services.AddScoped<IDisposer, Disposer>();
                services.AddSingleton<IValue<WidgetAvailability>, Value<WidgetAvailability>>();
            });
    }

    public static IWidgetBuilder Create() => new WidgetBuilder();

    public IWidgetBuilder Configuration<TConfiguration>(Action<TConfiguration> configurationDelegate)
        where TConfiguration : 
        WidgetConfiguration,
        new()
    {
        TConfiguration configuration = new TConfiguration();
        configurationDelegate(configuration);

        hostBuilder.ConfigureServices(services =>
        {
            services.AddHandler<WidgetConfigurationHandler>();
            services.AddConfiguration<WidgetConfiguration>(section: configuration.GetType().Name, configuration: configuration);
            services.AddConfiguration(configuration);
        });

        return this;
    }

    public IWidgetBuilder UseViewModelTemplate<TWidgetContent, TWidgetTemplate>()
        where TWidgetContent :
        IWidgetViewModel
    {
        hostBuilder.ConfigureServices(services =>
        {
            services.AddWidgetTemplate<TWidgetContent, TWidgetTemplate>();
        });

        return this;
    }
    public IWidgetHost Build()
    {
        IHost host = hostBuilder.Build();
        return host.Services.GetRequiredService<IWidgetHost>();
    }

    public IWidgetBuilder ConfigureServices(Action<IServiceCollection> configureDelegate)
    {
        hostBuilder.ConfigureServices(configureDelegate);
        return this;
    }

    public IWidgetBuilder UseViewModel<TViewModel>() 
        where TViewModel : 
        IWidgetViewModel
    {
        hostBuilder.ConfigureServices(services =>
        {
            services.AddWidgetTemplate<TViewModel>();
        });

        return this;
    }
}
