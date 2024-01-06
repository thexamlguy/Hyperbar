using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Hyperbar.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddConfiguration<TConfiguration>(this IServiceCollection services,
        string path = "Settings.json")
        where TConfiguration :
        class, new()
    {
        return services.AddConfiguration<TConfiguration>(typeof(TConfiguration).Name, path);
    }

    public static IServiceCollection AddConfiguration<TConfiguration>(this IServiceCollection services,
        string section,
        string path = "Settings.json",
        Action<JsonSerializerOptions>? serializerDelegate = null)
        where TConfiguration :
        class, new()
    {
        _ = services.AddOptions();
        _ = services.AddSingleton<IConfigureOptions<TConfiguration>>(new ConfigureNamedOptions<TConfiguration>("", args => { }));

        _ = services.AddTransient<IConfigurationWriter<TConfiguration>>(provider =>
        {
            string? jsonFilePath = null;
            if (provider.GetService<IHostEnvironment>() is IHostEnvironment hostEnvironment)
            {
                IFileProvider fileProvider = hostEnvironment.ContentRootFileProvider;
                IFileInfo fileInfo = fileProvider.GetFileInfo(path);

                jsonFilePath = fileInfo.PhysicalPath;
            }

            jsonFilePath ??= Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);

            JsonSerializerOptions? defaultSerializerOptions = null;
            if (serializerDelegate is not null)
            {
                defaultSerializerOptions = new JsonSerializerOptions();
                serializerDelegate.Invoke(defaultSerializerOptions);
            }

            return new ConfigurationWriter<TConfiguration>(jsonFilePath, section, defaultSerializerOptions);
        });

        _ = services.AddTransient<IWritableConfiguration<TConfiguration>, WritableConfiguration<TConfiguration>>();
        return services;
    }

    public static IServiceCollection AddWidgetTemplate<TWidgetContent>(this IServiceCollection services)
        where TWidgetContent :
        IWidgetViewModel
    {
        Type contentType = typeof(TWidgetContent);
        Type templateType = typeof(IWidgetView);

        string key = contentType.Name;

        _ = services.AddTransient(typeof(IWidgetViewModel), contentType);
        services.TryAddTransient(templateType, provider => provider.GetService<IWidgetView>()!);

        _ = services.AddKeyedTransient(typeof(IWidgetViewModel), key, contentType);
        services.TryAddKeyedTransient(key, (provider, key) => provider.GetService<IWidgetView>()!);

        _ = services.AddTransient<IContentTemplateDescriptor>(provider => new ContentTemplateDescriptor
        {
            ContentType = contentType,
            TemplateType = templateType,
            Key = key
        });

        return services;
    }

    public static IServiceCollection AddWidgetTemplate<TWidgetContent, TWidgetTemplate>(this IServiceCollection services)
        where TWidgetContent :
        IWidgetViewModel
    {
        Type contentType = typeof(TWidgetContent);
        Type templateType = typeof(TWidgetTemplate);

        string key = contentType.Name;

        _ = services.AddTransient(typeof(IWidgetViewModel), contentType);
        services.TryAddTransient(templateType);

        _ = services.AddKeyedTransient(typeof(IWidgetViewModel), key, contentType);
        services.TryAddKeyedTransient(templateType, key);

        _ = services.AddTransient<IContentTemplateDescriptor>(provider => new ContentTemplateDescriptor
        {
            ContentType = contentType,
            TemplateType = templateType,
            Key = key
        });

        return services;
    }

    public static IServiceCollection AddContentTemplate<TContent, TTemplate>(this IServiceCollection services,
        object? key = null)
    {
        Type contentType = typeof(TContent);
        Type templateType = typeof(TTemplate);

        key ??= contentType.Name;

        _ = services.AddTransient(contentType);
        services.TryAddTransient(templateType);

        _ = services.AddKeyedTransient(contentType, key);
        _ = services.AddKeyedTransient(templateType, key);

        _ = services.AddTransient<IContentTemplateDescriptor>(provider => new ContentTemplateDescriptor
        {
            ContentType = contentType,
            TemplateType = templateType,
            Key = key
        });

        return services;
    }
}