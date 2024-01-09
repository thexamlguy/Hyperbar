namespace Hyperbar;

public interface IWritableConfiguration<out TConfiguration>
    where TConfiguration :
    class, new()
{
    void Write(Action<TConfiguration> updateDelegate);
}
