using System.Collections.Concurrent;

namespace Hyperbar;

public class SubscriptionCollection :
    ConcurrentDictionary<object, List<WeakReference>>;
