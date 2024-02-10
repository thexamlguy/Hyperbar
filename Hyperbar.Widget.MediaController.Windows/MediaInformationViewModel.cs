using CommunityToolkit.Mvvm.ComponentModel;

namespace Hyperbar.Widget.MediaController.Windows;

public partial class MediaInformationViewModel(IServiceProvider serviceProvider,
    IServiceFactory serviceFactory,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer) :
    WidgetComponentViewModel(serviceProvider, serviceFactory, publisher, subscriber, disposer),
    INotificationHandler<Changed<MediaInformation>>
{
    [ObservableProperty]
    private string? description;

    [ObservableProperty]
    private byte[]? image;

    [ObservableProperty]
    private string? title;

    public Task Handle(Changed<MediaInformation> notification, 
        CancellationToken cancellationToken)
    {
        if (notification.Value is MediaInformation value)
        {
            Title = value.Title;
            Description = value.Description;
            Image = value.Image;
        }

        return Task.CompletedTask;
    }

    public override async Task InitializeAsync() =>
        await Publisher.PublishAsync<Request<MediaInformation>>();
}
