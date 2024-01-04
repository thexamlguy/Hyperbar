using Microsoft.Extensions.Hosting;
using System.Collections.ObjectModel;

namespace Hyperbar;

public class ObservableCollectionViewModel : 
    ObservableCollection<object>
{

}

public class AppService(IEnumerable<IInitializer> initializers) : 
    IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        foreach (var initializer in initializers)
        {
            await initializer.InitializeAsync();
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}
