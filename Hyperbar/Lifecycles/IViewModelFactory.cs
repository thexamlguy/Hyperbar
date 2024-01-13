namespace Hyperbar;

public interface IViewModelFactory<TParameter, TViewModel>
{
    TViewModel? Create(TParameter value);
}
