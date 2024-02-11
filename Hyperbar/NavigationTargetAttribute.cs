namespace Hyperbar.UI.Windows;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class NavigationTargetAttribute(string name) : Attribute
{
    public string Name => name;
}
