namespace Hyperbar;

public interface INavigationViewModel : 
    IObservableViewModel
{
    string Text { get; set; }
}
