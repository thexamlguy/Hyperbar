using Microsoft.Extensions.Hosting;

namespace Hyperbar.Widget;

public interface IWidgetHost : 
    IHost
{
    WidgetConfiguration Configuration { get; }
}
