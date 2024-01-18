namespace Hyperbar.Windows.Primary;

public class WidgetComponentViewModelProvider(ICache<Guid, IWidgetComponentViewModel> cache) :
    IProvider<PrimaryCommandConfiguration, IWidgetComponentViewModel?>
{
    public IWidgetComponentViewModel? Get(PrimaryCommandConfiguration value)
    {
        if (cache.TryGetValue(value.Id, out IWidgetComponentViewModel? viewModel))
        {
            return viewModel;
        }

        return default;
    }
}
