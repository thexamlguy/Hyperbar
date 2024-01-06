using Hyperbar.Lifecycles;
using Hyperbar.Options;
using Hyperbar.Templates;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Hyperbar;
public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddWritableConfiguration<TConfiguration>(this IServiceCollection services,
        string path = "Settings.json")
        where TConfiguration :
        class, new()
    {
        return services.AddWritableConfiguration<TConfiguration>(typeof(TConfiguration).Name, path);
    }

    public static IServiceCollection AddWritableConfiguration<TConfiguration>(this IServiceCollection services,
        string section,
        string path = "Settings.json",
        Action<JsonSerializerOptions>? serializerDelegate = null) 
        where TConfiguration : 
        class, new()
    {
        services.AddOptions();
        services.AddSingleton<IConfigureOptions<TConfiguration>>(new ConfigureNamedOptions<TConfiguration>("", args => { }));

        services.AddTransient<IConfigurationWriter<TConfiguration>>(provider =>
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

        services.AddTransient<IWritableConfiguration<TConfiguration>, WritableConfiguration<TConfiguration>>();
        return services;
    }

    public static IServiceCollection AddCommandTemplate<TCommand, TCommandTemplate>(this IServiceCollection services)
        where TCommand :
        ICommandWidgetViewModel
    {
        Type dataType = typeof(TCommand);
        Type templateType = typeof(TCommandTemplate);

        string key = dataType.Name;
        
        services.AddTransient(typeof(ICommandWidgetViewModel), dataType);
        services.AddTransient(templateType);
        services.AddKeyedTransient(typeof(ICommandWidgetViewModel), key, dataType);
        services.AddKeyedTransient(templateType, key);
        
        services.AddTransient<IDataTemplateDescriptor>(provider => new DataTemplateDescriptor
        {
            DataType = dataType,
            TemplateType = templateType,
            Key = key
        });

        return services;
    }

    public static IServiceCollection AddDataTemplate<TData, TTemplate>(this IServiceCollection services,
        object? key = null)
    {
        Type dataType = typeof(TData);
        Type templateType = typeof(TTemplate);

        key ??= dataType.Name;

        services.AddKeyedTransient(dataType, key);
        services.AddKeyedTransient(templateType, key);
        
        services.AddTransient<IDataTemplateDescriptor>(provider => new DataTemplateDescriptor
        {
            DataType = dataType,
            TemplateType = templateType,
            Key = key
        });

        return services;
    }
}
