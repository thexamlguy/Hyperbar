using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar;

public class NavigateHandler :
    INotificationHandler<Navigate>
{
    private readonly IEnumerable<IContentTemplateDescriptor> contentTemplateDescriptors;
    private readonly IServiceProvider provider;
    private readonly IMediator mediator;
    private readonly IEnumerable<INavigationDescriptor> navigationDescriptors;

    public NavigateHandler(IServiceProvider provider,
        IMediator mediator,
        IEnumerable<INavigationDescriptor> navigationDescriptors,
        IEnumerable<IContentTemplateDescriptor> contentTemplateDescriptors)
    {
        this.provider = provider;
        this.mediator = mediator;
        this.navigationDescriptors = navigationDescriptors;
        this.contentTemplateDescriptors = contentTemplateDescriptors;
    }

    public async Task Handle(Navigate args, 
        CancellationToken cancellationToken)
    {
        if (contentTemplateDescriptors.FirstOrDefault(x => x.Key == args.Key)
            is IContentTemplateDescriptor contentTemplateDescriptor)
        {
            if (navigationDescriptors.FirstOrDefault(x => contentTemplateDescriptor.TemplateType == x.Type ||
                contentTemplateDescriptor.TemplateType.BaseType == x.Type) is { } navigationDescriptor)
            {
                if (contentTemplateDescriptor.TemplateType == navigationDescriptor.Type ||
                    contentTemplateDescriptor.TemplateType.BaseType == navigationDescriptor.Type)
                {
                    if (provider.GetRequiredKeyedService(contentTemplateDescriptor.TemplateType,
                        contentTemplateDescriptor.Key) is { } template)
                    {
                        Type navigateType = typeof(Navigate<>)
                            .MakeGenericType(navigationDescriptor.Type);

                        if (Activator.CreateInstance(navigateType, 
                            new object[] { template, args.Key }) is object navigate)
                        {
                            await mediator.PublishAsync(navigate, cancellationToken);
                        }
                    }
                }
            }
        }
    }
}

