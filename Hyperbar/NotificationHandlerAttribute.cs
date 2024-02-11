namespace Hyperbar;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class NotificationHandlerAttribute(object key) : Attribute
{
    public object Key => key;
}
