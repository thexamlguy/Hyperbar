﻿using Hyperbar.UI.Windows;

namespace Hyperbar.Windows;

public partial class SettingsViewModel :
    ObservableCollectionViewModel<INavigationViewModel>
{
    public SettingsViewModel(IViewModelTemplateSelector viewModelTemplateSelector,
        IServiceProvider serviceProvider, 
        IServiceFactory serviceFactory,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer) : base(serviceProvider, serviceFactory, publisher, subscriber, disposer)
    {
        ViewModelTemplateSelector = viewModelTemplateSelector;

        Add<GeneralSettingsNavigationViewModel>("General");
        Add<WidgetSettingsNavigationViewModel>("Widgets");
    }

    public IViewModelTemplateSelector ViewModelTemplateSelector { get; }
}
