namespace Hyperbar.Lifecycles;

public interface ICommandContext
{
    IServiceProvider ServiceProvider { get; }
}
