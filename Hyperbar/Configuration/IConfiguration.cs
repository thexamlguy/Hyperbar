namespace Hyperbar;

public interface IConfiguration<out TConfiguration>
    where TConfiguration :
    class, new()
{
    TConfiguration Value { get; }
}
