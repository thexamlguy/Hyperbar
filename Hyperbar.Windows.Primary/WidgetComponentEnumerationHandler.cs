using Hyperbar.Widget;

namespace Hyperbar.Windows.Primary;

public class WidgetComponentEnumerationHandler(PrimaryWidgetConfiguration configuration,
    IFactory<PrimaryCommandConfiguration, IWidgetComponentViewModel?> factory,
    ICache<(Guid ParentId, Guid Id), PrimaryCommandConfiguration> cache) :
    IEnumerator<IWidgetComponentViewModel>
{
    public IEnumerable<IWidgetComponentViewModel?> Get()
    {
        Stack<(Guid, List<PrimaryCommandConfiguration>)> stack = new();
        stack.Push((Guid.Empty, configuration.Commands));

        while (stack.Count > 0)
        {
            (Guid currentParentId, List<PrimaryCommandConfiguration> currentConfigurations) = stack.Pop();
            foreach (PrimaryCommandConfiguration configuration in currentConfigurations)
            {
                cache.Add((currentParentId, configuration.Id), configuration);
                if (configuration.Commands is not null && configuration.Commands.Count > 0)
                {
                    stack.Push((configuration.Id, configuration.Commands));
                }
            }
        }

        foreach (PrimaryCommandConfiguration item in configuration.Commands.OrderBy(x => x.Order))
        {
            yield return factory.Create(item);
        }
    }
}
