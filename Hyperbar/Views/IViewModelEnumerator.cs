namespace Hyperbar;

public interface IViewModelEnumerator<TItem>
{
    IAsyncEnumerable<TItem?> Next();
}
