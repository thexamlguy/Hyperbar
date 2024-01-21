namespace Hyperbar;

public class WidgetFactory : 
    IFactory<Type, IWidget>
{
    public IWidget? Create(Type value)
    {
        if (Activator.CreateInstance(value) is IWidget widget)
        {
            return widget;
        }

        return default;
    }
}
