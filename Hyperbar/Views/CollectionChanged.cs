using System.Collections;

namespace Hyperbar;

public record CollectionChanged<TCollection>(TCollection Items) : INotification where TCollection : IEnumerable;
