namespace Hyperbar;

public interface IEnumerator<TItem>
{
    IEnumerable<TItem?> Next();
}
