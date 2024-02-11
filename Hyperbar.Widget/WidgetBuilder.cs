using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace Hyperbar.Widget;

public class WidgetBuilder : 
    IWidgetBuilder
{
    private readonly IHostBuilder hostBuilder;

    private bool configurationRegistered;

    private bool viewModelTemplateRegistered;

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

                services.AddScoped<SubscriptionCollection>();
                services.AddScoped<ISubscriptionManager, SubscriptionManager>();
                services.AddTransient<ISubscriber, Subscriber>();
                services.AddTransient<IPublisher, Publisher>();

                services.AddScoped<IMediator, Mediator>();
                services.AddScoped<IDisposer, Disposer>();

                services.AddHandler<WidgetAvailabilityChangedHandler>();
                services.AddValueChangedNotification<WidgetConfiguration,
                    WidgetAvailability>((config) => (args) =>
                    {
                        args.Value = config.IsEnabled;
                    });
            });
    }

    public static IWidgetBuilder Create() => new WidgetBuilder();

    public IWidgetHost Build()
    {
        IHost host = hostBuilder.Build();
        return host.Services.GetRequiredService<IWidgetHost>();
    }

    public IWidgetBuilder UseConfiguration<TConfiguration>(Action<TConfiguration> configurationDelegate)
        where TConfiguration : 
        WidgetConfiguration,
        new()
    {
        if (configurationRegistered)
        {
            return this;
        }

        configurationRegistered = true;
        TConfiguration configuration = new();
        configurationDelegate(configuration);

        hostBuilder.ConfigureServices(services =>
        {
            services.AddHandler<WidgetConfigurationHandler>();
            services.AddConfiguration<WidgetConfiguration>(section: configuration.GetType().Name,
                configuration: configuration);

            services.AddConfiguration(configuration);
        });

        return this;
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
        if (viewModelTemplateRegistered)
        {
            return this;
        }

        viewModelTemplateRegistered = true;
        hostBuilder.ConfigureServices(services =>
        {
            services.AddWidgetTemplate<TViewModel>();
        });

        return this;
    }

    public IWidgetBuilder UseViewModelTemplate<TWidgetContent, TWidgetTemplate>()
        where TWidgetContent :
        IWidgetViewModel
    {
        if (viewModelTemplateRegistered)
        {
            return this;
        }

        viewModelTemplateRegistered = true;
        hostBuilder.ConfigureServices(services =>
        {
            services.AddWidgetTemplate<TWidgetContent, TWidgetTemplate>();
        });

        return this;
    }
}
