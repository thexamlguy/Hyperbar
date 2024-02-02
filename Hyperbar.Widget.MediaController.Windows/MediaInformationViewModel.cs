using CommunityToolkit.Mvvm.ComponentModel;

namespace Hyperbar.Widget.MediaController.Windows;

public partial class MediaInformationViewModel(IServiceFactory serviceFactory,
    IMediator mediator,
    IDisposer disposer,
    ITemplateFactory templateFactory) :
    WidgetComponentViewModel(serviceFactory, mediator, disposer, templateFactory),
    INotificationHandler<Changed<MediaInformation>>
{
    [ObservableProperty]
    private string? description;

    [ObservableProperty]
    private Stream? thumbnailSource;

    [ObservableProperty]
    private string? title;

    public Task Handle(Changed<MediaInformation> notification, 
        CancellationToken cancellationToken)
    {
        if (notification.Value is MediaInformation value)
        {
            Title = value.Title;
            Description = value.Description;
            ThumbnailSource = value.ThumbnailSource;
        }

        return Task.CompletedTask;
    }

    public override async Task OnInitializeAsync() =>
        await Mediator.PublishAsync<Request<MediaInformation>>();
}
