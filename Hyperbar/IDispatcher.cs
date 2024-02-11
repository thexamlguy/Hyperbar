namespace Hyperbar;

public interface IDispatcher
{
    Task InvokeAsync(Action action);
}
