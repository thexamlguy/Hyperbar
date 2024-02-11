
namespace Hyperbar;

public record Remove<TValue>(TValue Value) : 
    INotification;