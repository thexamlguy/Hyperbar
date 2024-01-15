using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace Hyperbar.Windows.MediaController;

public partial class MediaInformationViewModel(IServiceFactory serviceFactory,
    IMediator mediator,
    IDisposer disposer,
    ITemplateFactory templateFactory) :
    WidgetComponentViewModel(serviceFactory, mediator, disposer, templateFactory),   
    IViewModelInitialization,
    INotificationHandler<Changed<MediaInformation>>
{
    [ObservableProperty]
    private string? description;

    [ObservableProperty]
    private string? title;

    public ICommand Initialize => 
        new AsyncRelayCommand(InitializeAsync);

    public ValueTask Handle(Changed<MediaInformation> notification, 
        CancellationToken cancellationToken)
    {
        if (notification.Value is MediaInformation value)
        {
            Title = value.Title;
            Description = value.Description;
        }

        return ValueTask.CompletedTask;
    }

    public async Task InitializeAsync() => 
        await Mediator.PublishAsync<Request<MediaInformation>>();
}
