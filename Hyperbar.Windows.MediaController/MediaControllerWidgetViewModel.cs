﻿namespace Hyperbar.Windows.MediaController;

public class MediaControllerWidgetViewModel(ITemplateFactory templateFactory,
    IServiceFactory serviceFactory,
    IMediator mediator,
    IDisposer disposer) :
    ObservableCollectionViewModel<MediaControllerViewModel>(serviceFactory, mediator, disposer),
    IWidgetViewModel,
    ITemplatedViewModel
{
    public ITemplateFactory TemplateFactory => templateFactory;
}