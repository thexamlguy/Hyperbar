using Hyperbar.Lifecycles;
using Hyperbar.Templates;
using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar;
public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddCommandTemplate<TCommand, TCommandTemplate>(this IServiceCollection services)
        where TCommand :
        ICommandViewModel
    {
        Type dataType = typeof(TCommand);
        Type templateType = typeof(TCommandTemplate);

        string key = dataType.Name;

        _ = services.AddTransient(typeof(ICommandViewModel), dataType);
        _ = services.AddTransient(templateType);

        _ = services.AddKeyedTransient(typeof(ICommandViewModel), key, dataType);
        _ = services.AddKeyedTransient(templateType, key);

        _ = services.AddTransient<IDataTemplateDescriptor>(provider => new DataTemplateDescriptor
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

        _ = services.AddKeyedTransient(dataType, key);
        _ = services.AddKeyedTransient(templateType, key);

        _ = services.AddTransient<IDataTemplateDescriptor>(provider => new DataTemplateDescriptor
        {
            DataType = dataType,
            TemplateType = templateType,
            Key = key
        });

        return services;
    }
}
