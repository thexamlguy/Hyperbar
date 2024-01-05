using Microsoft.Extensions.Hosting;
using System.Collections.ObjectModel;

namespace Hyperbar.Lifecycles;

public class ObservableCollectionViewModel :
    ObservableCollection<object>
{

}

public class AppService(IEnumerable<IInitializer> initializers) :
    IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        foreach (IInitializer initializer in initializers)
        {
            await initializer.InitializeAsync();
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
