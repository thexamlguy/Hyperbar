using Microsoft.Extensions.Options;

namespace Hyperbar;

public interface IWritableConfiguration<out TConfiguration> : 
    IOptionsSnapshot<TConfiguration> 
    where TConfiguration :
    class, new()
{
    void Update(Action<TConfiguration?> updateAction, 
        bool reload = true);
}
