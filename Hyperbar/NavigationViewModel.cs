﻿using CommunityToolkit.Mvvm.ComponentModel;

namespace Hyperbar;

public partial class NavigationViewModel(IServiceProvider serviceProvider,
    IServiceFactory serviceFactory,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    string text) :
    ObservableCollectionViewModel<INavigationViewModel>(serviceProvider, serviceFactory, publisher, subscriber, disposer),
    INavigationViewModel
{
    [ObservableProperty]
    private string? text = text;
}

public partial class NavigationViewModel<TNavigationViewModel>(IServiceProvider serviceProvider,
    IServiceFactory serviceFactory,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    string text) :
    ObservableCollectionViewModel<TNavigationViewModel>(serviceProvider, serviceFactory, publisher, subscriber, disposer),
    INavigationViewModel
    where TNavigationViewModel :
    INavigationViewModel
{
    [ObservableProperty]
    private string? text = text;
}
