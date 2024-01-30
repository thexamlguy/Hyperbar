﻿using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Media;

namespace Hyperbar.Widget.MediaController.Windows;

public partial class MediaInformationViewModel(IServiceFactory serviceFactory,
    IMediator mediator,
    IDisposer disposer,
    ITemplateFactory templateFactory) :
    WidgetComponentViewModel(serviceFactory, mediator, disposer, templateFactory),
    IInitialization,
    INotificationHandler<Changed<MediaInformation>>
{
    [ObservableProperty]
    private string? description;

    [ObservableProperty]
    private Stream? thumbnailSource;

    [ObservableProperty]
    private string? title;

    public Task Handle(Changed<MediaInformation> notification, 
        CancellationToken cancellationToken)
    {
        if (notification.Value is MediaInformation value)
        {
            Title = value.Title;
            Description = value.Description;
            ThumbnailSource = value.ThumbnailSource;
        }

        return Task.CompletedTask;
    }

    public override async Task InitializeAsync() =>
        await Mediator.PublishAsync<Request<MediaInformation>>();
}