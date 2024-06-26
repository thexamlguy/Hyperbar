﻿using Hyperbar.UI.Windows;

namespace Hyperbar.Widget;


[NotificationHandler(nameof(IWidgetHostViewModel))]
public partial class ApplicationBarViewModel :
    ObservableCollectionViewModel<IDisposable>
{
    public ApplicationBarViewModel(IViewModelTemplateSelector viewModelTemplateSelector, 
        IServiceProvider serviceProvider,
        IServiceFactory serviceFactory,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer) : base(serviceProvider, serviceFactory, publisher, subscriber, disposer)
    {
        ViewModelTemplateSelector = viewModelTemplateSelector;

        Add<PrimaryViewModel>(0);
        Add<SecondaryViewModel>(1);
    }

    public IViewModelTemplateSelector ViewModelTemplateSelector { get; }
}