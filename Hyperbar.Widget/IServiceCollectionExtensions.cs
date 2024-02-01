using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Hyperbar.Widget;

public static class IServiceCollectionExtensions
{ 
    public static IServiceCollection AddWidget(this IServiceCollection services)
    {
        services.AddTransient<IInitialization, WidgetExtensionInitializer>();
        services.AddTransient<IFactory<Type, IWidget>, WidgetFactory>();

        services.AddHandler<WidgetExtensionEnumerator>();
        services.AddHandler<WidgetExtensionHandler>();
        services.AddHandler<WidgetHostHandler>();
        services.AddHandler<WidgetStartedHandler>();

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

    public static IServiceCollection AddWidgetTemplate<TWidgetContent, 
        TWidgetTemplate>(this IServiceCollection services)
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

        services.AddTransient<IContentTemplateDescriptor>(provider =>
            new ContentTemplateDescriptor { ContentType = contentType, 
                TemplateType = templateType, Key = key });

        return services;
    }
}