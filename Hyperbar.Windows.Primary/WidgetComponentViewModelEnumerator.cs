namespace Hyperbar.Windows.Primary;

public class WidgetComponentViewModelEnumerator(PrimaryWidgetConfiguration configuration,
    IFactory<PrimaryCommandConfiguration, IWidgetComponentViewModel?> factory) :
    IViewModelEnumerator<IWidgetComponentViewModel>
{
    public IEnumerable<IWidgetComponentViewModel?> Next()
    {
        foreach (PrimaryCommandConfiguration item in configuration.OrderBy(x => x.Order))
        {
            yield return factory.Create(item);
        }
    }
}
