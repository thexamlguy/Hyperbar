using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar;

public static class IServiceCollectionExtensions
{
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
