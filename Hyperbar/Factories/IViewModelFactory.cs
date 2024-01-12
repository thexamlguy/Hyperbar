namespace Hyperbar;

public interface IViewModelFactory<TIn, TOut>
{
    ValueTask<TOut> CreateAsync(TIn value);
}