namespace Hyperbar.Widget;

public interface IWidgetHostCollection : 
    IEnumerable<IWidgetHost>
{
    void Add(IWidgetHost widgetHost);
}
