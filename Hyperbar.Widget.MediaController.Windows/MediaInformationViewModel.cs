using CommunityToolkit.Mvvm.ComponentModel;

namespace Hyperbar.Widget.MediaController.Windows;

public partial class MediaInformationViewModel(IServiceProvider serviceProvider,
    IServiceFactory serviceFactory,
    IMediator mediator,
    IDisposer disposer) :
    WidgetComponentViewModel(serviceProvider, serviceFactory, mediator, disposer),
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
        await Mediator.PublishAsync<Request<MediaInformation>>();
}
