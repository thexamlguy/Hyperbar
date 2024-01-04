using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hyperbar;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddCommandTemplate<TCommand, TCommandTemplate>(this IServiceCollection services)
        where TCommand :
        ICommandViewModel
    {
        Type dataType = typeof(TCommand);
        Type templateType = typeof(TCommandTemplate);

        var key = dataType.Name;

        services.AddTransient(typeof(ICommandViewModel), dataType);
        services.AddTransient(templateType);

        services.AddKeyedTransient(typeof(ICommandViewModel), key, dataType);
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
