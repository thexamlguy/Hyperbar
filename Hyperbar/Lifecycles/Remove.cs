
namespace Hyperbar;

public record Remove<TValue>(TValue Value) : INotification;

public record Navigate(object Key) :
    INotification;

public class NavigateHandler :
    INotificationHandler<Navigate>
{
    private readonly IEnumerable<IContentTemplateDescriptor> descriptors;

    public NavigateHandler(IEnumerable<IContentTemplateDescriptor> descriptors,
        IServiceProvider provider)
    {
        this.descriptors = descriptors;
    }

    public Task Handle(Navigate args, 
        CancellationToken cancellationToken)
    {
        if (descriptors.FirstOrDefault(x => x.Key == args.Key)
            is IContentTemplateDescriptor descriptor)
        {
            //if (provider.GetRequiredKeyedService(descriptor.TemplateType,
            //    descriptor.Key) is { } template)
            //{
            //    return template;
            //}
        }

        throw new NotImplementedException();
    }
}

