using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Hyperbar;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddHandler<THandler>(this IServiceCollection services, 
        ServiceLifetime lifetime = ServiceLifetime.Transient) 
        where THandler :
        IHandler
    {
        if (typeof(THandler).GetInterface(typeof(INotificationHandler<>).Name) is { } notificationContract)
        {
            if (notificationContract.GetGenericArguments() is { Length: 1 } arguments)
            {
                Type notificationType = arguments[0];

                services.TryAdd(new ServiceDescriptor(typeof(THandler), typeof(THandler), lifetime));
                services.Add(new ServiceDescriptor(typeof(INotificationHandler<>).MakeGenericType(notificationType),
                    provider => provider.GetRequiredService<THandler>(), lifetime));
            }
        }

        if (typeof(THandler).GetInterface(typeof(IHandler<,>).Name) is { } requestContract)
        {
            if (requestContract.GetGenericArguments() is { Length: 2 } arguments)
            {
                Type requestType = arguments[0];
                Type responseType = arguments[1];

                Type wrapperType = typeof(RequestClassHandlerWrapper<,>).MakeGenericType(requestType, responseType);

                services.TryAdd(new ServiceDescriptor(typeof(THandler), typeof(THandler), lifetime));
                services.Add(new ServiceDescriptor(wrapperType,
                    provider => provider.GetService<IServiceFactory>()?.Create(wrapperType,
                        provider.GetRequiredService<THandler>(),
                        provider.GetServices(typeof(IPipelineBehavior<,>).MakeGenericType(requestType, responseType)))!,
                    lifetime
                ));
            }
        }

        if (typeof(THandler).GetInterface(typeof(IMappingHandler<,>).Name) is { } mappingContract)
        {
            if (mappingContract.GetGenericArguments() is { Length: 2 } arguments)
            {
                Type responseType = arguments[1];

                services.AddTransient(typeof(THandler));
                services.AddTransient(responseType, provider =>
                {
                    return ((dynamic)provider.GetRequiredService<THandler>()).Handle();
                });
            }
        }

        return services;
    }

    public static IServiceCollection AddConfiguration<TConfiguration>(this IServiceCollection services,
        IConfiguration configuration)
        where TConfiguration :
        class, new()
    {
        return services.AddConfiguration<TConfiguration>(configuration, typeof(TConfiguration).Name, "Settings.json", null);
    }

    public static IServiceCollection AddConfiguration<TConfiguration>(this IServiceCollection services,
        IConfiguration configuration,
        TConfiguration? defaults = null)
        where TConfiguration :
        class, new()
    {
        return services.AddConfiguration(configuration, typeof(TConfiguration).Name, "Settings.json", defaults);
    }

    public static IServiceCollection AddConfiguration<TConfiguration>(this IServiceCollection services,
        IConfiguration configuration,
        string section,
        string path = "Settings.json",
        TConfiguration? defaults = null,
        Action<JsonSerializerOptions>? serializerDelegate = null)
        where TConfiguration :
        class, new()
    {
        services.Configure<TConfiguration>(configuration);
        services.AddSingleton<IConfigureOptions<TConfiguration>>(new ConfigureNamedOptions<TConfiguration>("", args => { }));
        services.AddTransient(provider => provider.GetService<IOptionsMonitor<TConfiguration>>()!.CurrentValue);

        services.AddSingleton<IConfigurationWriter<TConfiguration>>(provider =>
        {
            string? jsonFilePath = null;
            if (provider.GetService<IHostEnvironment>() is IHostEnvironment hostEnvironment)
            {
                IFileProvider fileProvider = hostEnvironment.ContentRootFileProvider;
                IFileInfo fileInfo = fileProvider.GetFileInfo(path);

                jsonFilePath = fileInfo.PhysicalPath;
            }

            jsonFilePath ??= Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);

            JsonSerializerOptions? defaultSerializer = null;
            if (serializerDelegate is not null)
            {
                defaultSerializer = new JsonSerializerOptions();
                serializerDelegate.Invoke(defaultSerializer);
            }

            return new ConfigurationWriter<TConfiguration>(jsonFilePath, section, defaultSerializer);
        });

        if (defaults is not null)
        {
        }

        services.AddTransient(provider => new DefaultConfiguration<TConfiguration>(defaults));
        services.AddTransient<IInitializer, ConfigurationInitializer<TConfiguration>>();

        services.AddTransient<IWritableConfiguration<TConfiguration>, WritableConfiguration<TConfiguration>>();
        return services;
    }

    public static IServiceCollection AddWidgetTemplate<TWidgetContent>(this IServiceCollection services)
        where TWidgetContent :
        IWidgetViewModel
    {
        Type contentType = typeof(TWidgetContent);
        Type templateType = typeof(IWidgetView);

        string key = contentType.Name;

        services.AddTransient(typeof(IWidgetViewModel), contentType);
        services.TryAddTransient(templateType, provider => provider.GetService<IWidgetView>()!);

        services.AddKeyedTransient(typeof(IWidgetViewModel), key, contentType);
        services.TryAddKeyedTransient(key, (provider, key) => provider.GetService<IWidgetView>()!);

        services.AddTransient<IContentTemplateDescriptor>(provider => new ContentTemplateDescriptor { ContentType = contentType, TemplateType = templateType, Key = key });

        return services;
    }

    public static IServiceCollection AddWidgetTemplate<TWidgetContent, TWidgetTemplate>(this IServiceCollection services)
        where TWidgetContent :
        IWidgetViewModel
    {
        Type contentType = typeof(TWidgetContent);
        Type templateType = typeof(TWidgetTemplate);

        string key = contentType.Name;

        services.AddTransient(typeof(IWidgetViewModel), contentType);
        services.TryAddTransient(templateType);

        services.AddKeyedTransient(typeof(IWidgetViewModel), key, contentType);
        services.TryAddKeyedTransient(templateType, key);

        services.AddTransient<IContentTemplateDescriptor>(provider => new ContentTemplateDescriptor { ContentType = contentType, TemplateType = templateType, Key = key });

        return services;
    }

    public static IServiceCollection AddContentTemplate<TContent, TTemplate>(this IServiceCollection services,
        object? key = null)
    {
        Type contentType = typeof(TContent);
        Type templateType = typeof(TTemplate);

        key ??= contentType.Name;

        services.AddTransient(contentType);
        services.TryAddTransient(templateType);

        services.AddKeyedTransient(contentType, key);
        services.AddKeyedTransient(templateType, key);

        services.AddTransient<IContentTemplateDescriptor>(provider => new ContentTemplateDescriptor { ContentType = contentType, TemplateType = templateType, Key = key });

        return services;
    }
}