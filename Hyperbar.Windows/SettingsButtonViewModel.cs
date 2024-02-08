using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Hyperbar.Windows;

public partial class SettingsButtonViewModel :
    ObservableViewModel
{
    [ObservableProperty]
    private IRelayCommand? invokeCommand;

    public SettingsButtonViewModel(IViewModelTemplateFactory templateFactory,
        IServiceProvider serviceProvider,
        IServiceFactory serviceFactory,
        IMediator mediator,
        IDisposer disposer) : base(serviceProvider, serviceFactory, mediator, disposer)
    {
        TemplateFactory = templateFactory;
        InvokeCommand = new AsyncRelayCommand(async () =>
            await mediator.PublishAsync(new Navigate("Settings")));
    }

    public IViewModelTemplateFactory TemplateFactory { get; }
}
