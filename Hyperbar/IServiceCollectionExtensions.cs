﻿using Hyperbar.UI.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

namespace Hyperbar;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddConfiguration<TConfiguration, 
        TValue>(this IServiceCollection services,
        Func<TConfiguration, Action<TValue>> changed)
        where TConfiguration :
        class
        where TValue : 
        class, new()
    {
        services.AddSingleton(new ConfigurationValue<TConfiguration, TValue>(changed));
        services.AddHandler<ConfigurationChangedHandler<TConfiguration, TValue>>();

        return services;
    }

    public static IServiceCollection AddCache<TKey, TValue>(this IServiceCollection services)
        where TKey :
        notnull
        where TValue :
        notnull
    {
        services.AddScoped<ICache<TKey, TValue>, Cache<TKey, TValue>>();
        services.AddTransient(provider => provider.GetService<ICache<TKey, TValue>>()!.Select(x => x.Value));

        return services;
    }

    public static IServiceCollection AddCache<TValue>(this IServiceCollection services)
    {
        services.AddSingleton<ICache<TValue>, Cache<TValue>>();
        services.AddTransient(provider => provider.GetService<ICache<TValue>>()!.Select(x => x));

        return services;
    }

    public static IServiceCollection AddConfiguration<TConfiguration>(this IServiceCollection services)
        where TConfiguration :
        class => services.AddConfiguration<TConfiguration>(typeof(TConfiguration).Name,
            "Settings.json",
            null);

    public static IServiceCollection AddConfiguration<TConfiguration>(this IServiceCollection services,
        Action<TConfiguration> configurationDelegate)
        where TConfiguration :
        class,
        new()
    {
        TConfiguration configuration = new();
        configurationDelegate.Invoke(configuration);

        return services.AddConfiguration(typeof(TConfiguration).Name,
            "Settings.json",
            configuration);
    }

    public static IServiceCollection AddConfiguration<TConfiguration>(this IServiceCollection services,
        TConfiguration configuration)
        where TConfiguration :
        class => services.AddConfiguration(configuration.GetType().Name,
            "Settings.json",
            configuration);

    public static IServiceCollection AddConfiguration<TConfiguration>(this IServiceCollection services,
        object configuration)
        where TConfiguration :
        class => services.AddConfiguration(configuration.GetType().Name,
            "Settings.json",
            (TConfiguration?)configuration);

    public static IServiceCollection AddConfiguration<TConfiguration>(this IServiceCollection services,
        string section,
        string path = "Settings.json",
        TConfiguration? configuration = null,
        Action<JsonSerializerOptions>? serializerDelegate = null)
        where TConfiguration :
        class
    {
        services.AddSingleton<IConfigurationSource<TConfiguration>>(provider =>
        {
            JsonSerializerOptions? defaultSerializer = null;
            if (serializerDelegate is not null)
            {
                defaultSerializer = new JsonSerializerOptions();
                serializerDelegate.Invoke(defaultSerializer);
            }

            return new ConfigurationSource<TConfiguration>(provider.GetRequiredService<IConfigurationFile<TConfiguration>>(), section, defaultSerializer);
        });

        services.AddSingleton<IConfigurationFile<TConfiguration>>(provider =>
        {
            IFileInfo? fileInfo = null;
            if (provider.GetService<IHostEnvironment>() is IHostEnvironment hostEnvironment)
            {
                IFileProvider fileProvider = hostEnvironment.ContentRootFileProvider;
                fileInfo = fileProvider.GetFileInfo(path);
            }

            fileInfo ??= new PhysicalFileInfo(new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path)));
            return new ConfigurationFile<TConfiguration>(fileInfo);
        });

        services.AddSingleton<IConfigurationMonitor<TConfiguration>, ConfigurationMonitor<TConfiguration>>();
        services.AddSingleton<IConfigurationReader<TConfiguration>, ConfigurationReader<TConfiguration>>();
        services.AddSingleton<IConfigurationWriter<TConfiguration>, ConfigurationWriter<TConfiguration>>();

        services.AddTransient<IConfigurationFactory<TConfiguration>>(provider => new ConfigurationFactory<TConfiguration>(() =>
            configuration ?? provider.GetRequiredService<TConfiguration>()));

        services.AddTransient<IInitializer, ConfigurationInitializer<TConfiguration>>();
        services.AddTransient<IConfigurationInitializer<TConfiguration>, ConfigurationInitializer<TConfiguration>>();

        services.AddTransient<IWritableConfiguration<TConfiguration>, WritableConfiguration<TConfiguration>>();

        services.AddTransient<IConfiguration<TConfiguration>, Configuration<TConfiguration>>();
        services.AddTransient(provider => provider.GetRequiredService<IConfiguration<TConfiguration>>().Value);

        return services;
    }

    public static IServiceCollection AddNavigationHandler<THandler>(this IServiceCollection services)
        where THandler : 
        INavigationHandler,
        IHandler
    {
        Type? contract = typeof(THandler).GetInterfaces()
            .FirstOrDefault(t => t.Name == typeof(INavigationHandler<>).Name);

        if (contract?.GetGenericArguments() is { Length: 1 } arguments)
        {
            services.AddTransient<INavigation>(provider => new Navigation
            {
                Type = arguments[0]
            });
        }

        services.AddHandler<THandler>();
        return services;
    }

    public static IServiceCollection AddContentTemplate<TContent, TTemplate>(this IServiceCollection services,
        object? key = null)
    {
        Type contentType = typeof(TContent);
        Type templateType = typeof(TTemplate);

        key ??= contentType.Name;

        services.AddTransient(contentType);
        services.AddTransient(templateType);

        services.AddKeyedTransient(contentType, key);
        services.AddKeyedTransient(templateType, key);

        services.AddTransient<IViewModelTemplate>(provider => new ViewModelTemplate
        {
            ViewModelType = contentType,
            ViewType = templateType,
            Key = key
        });

        return services;
    }

    public static IServiceCollection AddDefault(this IServiceCollection services)
    {
        services.AddSingleton<IServiceFactory>(provider =>
            new ServiceFactory((type, parameters) => ActivatorUtilities.CreateInstance(provider, type, parameters!)));

        services.AddSingleton<SubscriptionCollection>();
        services.AddSingleton<ISubscriptionManager, SubscriptionManager>();
        services.AddTransient<ISubscriber, Subscriber>();
        services.AddTransient<IPublisher, Publisher>();

        services.AddSingleton<IMediator, Mediator>();

        services.AddSingleton<IProxyService<IPublisher>>(provider =>
            new ProxyService<IPublisher>(provider.GetRequiredService<IPublisher>()));

        services.AddSingleton<IProxyService<INavigationTargetProvider>>(provider =>
            new ProxyService<INavigationTargetProvider>(provider.GetRequiredService<INavigationTargetProvider>()));

        services.AddSingleton<IDisposer, Disposer>();

        services.AddTransient<INavigationProvider, NavigationProvider>();
        services.AddSingleton<NavigationTargetCollection>();
        services.AddTransient<INavigationTargetProvider, NavigationTargetProvider>();
        services.AddHandler<NavigateHandler>();

        return services;
    }

    public static IServiceCollection AddHandler<THandler>(
        this IServiceCollection services,
        ServiceLifetime lifetime = ServiceLifetime.Transient)
        where THandler : IHandler
    {
        if (typeof(THandler).GetInterfaces() is { } contracts)
        {
            foreach (Type contract in contracts)
            {
                if (contract.Name == typeof(INotificationHandler<>).Name &&
                    contract.GetGenericArguments() is { Length: 1 } notificationArguments)
                {
                    Type notificationType = notificationArguments[0];
                    services.Add(new ServiceDescriptor(typeof(INotificationHandler<>).MakeGenericType(notificationType),
                        typeof(THandler),
                        lifetime));
                }

                if (contract.Name == typeof(IHandler<,>).Name &&
                    contract.GetGenericArguments() is { Length: 2 } handlerArguments)
                {
                    Type requestType = handlerArguments[0];
                    Type responseType = handlerArguments[1];

                    Type wrapperType = typeof(HandlerWrapper<,>).MakeGenericType(requestType, responseType);

                    services.TryAdd(new ServiceDescriptor(typeof(THandler), typeof(THandler), lifetime));
                    services.Add(new ServiceDescriptor(
                        wrapperType,
                        provider =>
                            provider.GetService<IServiceFactory>()?.Create(
                                wrapperType,
                                provider.GetRequiredService<THandler>(),
                                provider.GetServices(typeof(IPipelineBehavior<,>)
                                    .MakeGenericType(requestType, responseType))
                            )!,
                        lifetime
                    ));
                }
            }

            return services;
        }

        return services;
    }

    public static IServiceCollection AddRange(this IServiceCollection services,
            IServiceCollection fromServices)
    {
        foreach (ServiceDescriptor service in fromServices)
        {
            services.Add(service);
        }

        return services;
    }
}