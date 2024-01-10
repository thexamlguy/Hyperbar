using System.Collections.ObjectModel;

namespace Hyperbar;


public class ObservableCollectionViewModel<TItem> :
    ObservableCollection<TItem>,
    INotificationHandler<CollectionChanged<IEnumerable<TItem>>>
{
    private readonly IServiceFactory serviceFactory;
    private readonly SynchronizationContext? context;

    public ObservableCollectionViewModel(IServiceFactory serviceFactory, 
        IMediator mediator)
    {
        context = SynchronizationContext.Current;

        this.serviceFactory = serviceFactory;
        mediator.Subscribe(this);
    }

    public ObservableCollectionViewModel(IServiceFactory serviceFactory,
        IMediator mediator,
        IEnumerable<TItem> items)
    {
        context = SynchronizationContext.Current;

        this.serviceFactory = serviceFactory;
        mediator.Subscribe(this);

        AddRange(items);
    }

    public TItem Add()
    {
        TItem? item = serviceFactory.Create<TItem>();

        Add(item);
        return item;
    }

    public TItem Add<T>(params object?[] parameters)
        where T : TItem
    {
        T? item = serviceFactory.Create<T>(parameters);
        context?.Post(state => Add(item), null);

        return item;
    }

    public TItem Add<T>()
        where T :
        TItem
    {
        T? item = serviceFactory.Create<T>();
        context?.Post(state => Add(item), null);

        return item;
    }

    public void AddRange(IEnumerable<TItem> items)
    {
        foreach (TItem? item in items)
        {
            context?.Post(state => Add(item), null);
        }
    }

    public ValueTask Handle(CollectionChanged<IEnumerable<TItem>> notification, 
        CancellationToken cancellationToken)
    {
        context?.Post(state => Clear(), null);
        AddRange(notification.Items);

        return ValueTask.CompletedTask;
    }
}

public class ObservableCollectionViewModel(IServiceFactory serviceFactory, IMediator mediator) :
    ObservableCollectionViewModel<object>(serviceFactory, mediator);