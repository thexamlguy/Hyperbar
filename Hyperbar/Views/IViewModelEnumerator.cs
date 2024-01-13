namespace Hyperbar;

public interface IViewModelEnumerator<TItem>
{
    IEnumerable<TItem?> Next();
}
