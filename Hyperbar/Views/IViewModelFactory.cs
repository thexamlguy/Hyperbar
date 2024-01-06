namespace Hyperbar;

public interface IViewModelFactory<TFrom, TTo>
{
    TTo Create();
}
