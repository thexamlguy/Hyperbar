using Hyperbar.Widget;

namespace Hyperbar.Widget.Primary.Windows;

public class WidgetComponentProvider(ICache<Guid, IWidgetComponentViewModel> cache) :
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
