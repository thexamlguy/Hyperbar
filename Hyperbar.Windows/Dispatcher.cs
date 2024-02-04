using CommunityToolkit.WinUI;
using Microsoft.UI.Dispatching;

namespace Hyperbar.Windows;

public class Dispatcher(DispatcherQueue dispatcherQueue) :
    IDispatcher
{
    public async Task InvokeAsync(Action action) => 
        await dispatcherQueue.EnqueueAsync(action.Invoke);
}
