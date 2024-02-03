using CommunityToolkit.Mvvm.Input;

namespace Hyperbar.Widget.MediaController.Windows;

public interface IMediaButtonViewModel :
    IObservableViewModel
{
    IRelayCommand? InvokeCommand { get; set; }

    bool IsEnabled { get; set; }

    string? State { get; set; }
}
