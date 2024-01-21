using System.Reflection;

namespace Hyperbar;

public record WidgetAssembly(Assembly? Assembly = default) : 
    INotification;