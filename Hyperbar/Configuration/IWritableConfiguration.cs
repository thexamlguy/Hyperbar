using Microsoft.Extensions.Options;

namespace Hyperbar;

public interface IWritableConfiguration<out TConfiguration> :
    IOptionsSnapshot<TConfiguration>
    where TConfiguration :
    class, new()
{
    void Write(Action<TConfiguration> updateAction,
        bool reload = true);
}