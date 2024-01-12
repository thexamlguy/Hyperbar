namespace Hyperbar.Windows.Primary;

public class WidgetComponentViewModelEnumerator(PrimaryWidgetConfiguration configuration,
    IViewModelFactory<PrimaryCommandConfiguration, IWidgetComponentViewModel?> factory) :
    IViewModelEnumerator<IWidgetComponentViewModel>
{
    public async IAsyncEnumerable<IWidgetComponentViewModel?> Next()
    {
        foreach (PrimaryCommandConfiguration item in configuration)
        {
            yield return await factory.CreateAsync(item);
        }
    }
}
