namespace Hyperbar.Windows.Primary;

public class WidgetComponentViewModelEnumerator(PrimaryWidgetConfiguration configuration,
    IFactory<PrimaryCommandConfiguration, IWidgetComponentViewModel?> factory) :
    IViewModelEnumerator<IWidgetComponentViewModel>
{
    public IEnumerable<IWidgetComponentViewModel?> Next()
    {
        foreach (PrimaryCommandConfiguration item in configuration)
        {
            yield return factory.Create(item);
        }
    }
}
