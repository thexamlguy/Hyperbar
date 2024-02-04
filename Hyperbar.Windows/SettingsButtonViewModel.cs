using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Hyperbar.Windows;

public partial class SettingsButtonViewModel :
    ObservableViewModel,
    ITemplatedViewModel
{
    [ObservableProperty]
    private IRelayCommand? invokeCommand;

    public SettingsButtonViewModel(ITemplateFactory templateFactory,
        IServiceFactory serviceFactory,
        IMediator mediator,
        IDisposer disposer) : base(serviceFactory, mediator, disposer)
    {
        TemplateFactory = templateFactory;
        InvokeCommand = new AsyncRelayCommand(async () =>
            await mediator.PublishAsync(new Navigate("Settings")));
    }

    public ITemplateFactory TemplateFactory { get; }
}
