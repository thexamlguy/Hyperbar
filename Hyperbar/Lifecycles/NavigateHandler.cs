using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar;

public class NavigateHandler :
    INotificationHandler<Navigate>
{
    private readonly IViewModelTemplateDescriptorProvider contentTemplateDescriptors;
    private readonly IServiceProvider provider;
    private readonly IMediator mediator;
    private readonly IEnumerable<INavigationDescriptor> navigationDescriptors;

    public NavigateHandler(IServiceProvider provider,
        IMediator mediator,
        IEnumerable<INavigationDescriptor> navigationDescriptors,
        IViewModelTemplateDescriptorProvider contentTemplateDescriptors)
    {
        this.provider = provider;
        this.mediator = mediator;
        this.navigationDescriptors = navigationDescriptors;
        this.contentTemplateDescriptors = contentTemplateDescriptors;
    }

    public async Task Handle(Navigate args, 
        CancellationToken cancellationToken)
    {
        if (contentTemplateDescriptors.Get(args.Key)
            is IViewModelTemplateDescriptor contentTemplateDescriptor)
        {
            if (navigationDescriptors.FirstOrDefault(x => contentTemplateDescriptor.TemplateType == x.Type ||
                contentTemplateDescriptor.TemplateType.BaseType == x.Type) is { } navigationDescriptor)
            {
                if (contentTemplateDescriptor.TemplateType == navigationDescriptor.Type ||
                    contentTemplateDescriptor.TemplateType.BaseType == navigationDescriptor.Type)
                {
                    if (provider.GetRequiredKeyedService(contentTemplateDescriptor.TemplateType,
                        contentTemplateDescriptor.Key) is { } template && 
                        provider.GetRequiredKeyedService(contentTemplateDescriptor.ViewModelType,
                            contentTemplateDescriptor.Key) is { } content)
                    {
                        Type navigateType = typeof(Navigate<>)
                            .MakeGenericType(navigationDescriptor.Type);
                        if (Activator.CreateInstance(navigateType,
                            new object[] { template, content }) is object navigate)
                        {
                            await mediator.PublishAsync(navigate, cancellationToken);
                        }
                    }
                }
            }
        }
    }
}

