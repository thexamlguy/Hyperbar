using System.Collections;

namespace Hyperbar.Widget;

public class WidgetHostCollection :
    IWidgetHostCollection

{
    private readonly List<IWidgetHost> hosts = [];

    public void Add(IWidgetHost host)
    {
        hosts.Add(host);
    }

    public IEnumerator<IWidgetHost> GetEnumerator() =>
        hosts.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        hosts.GetEnumerator();
}
