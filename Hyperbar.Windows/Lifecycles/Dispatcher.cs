using CommunityToolkit.WinUI;
using Microsoft.UI.Dispatching;

namespace Hyperbar.Windows;

public class Dispatcher :
    IDispatcher
{
    private DispatcherQueue dispatcherQueue;

    public Dispatcher() => dispatcherQueue = DispatcherQueue.GetForCurrentThread();

    public async Task InvokeAsync(Action action) => await dispatcherQueue.EnqueueAsync(action.Invoke);
}
