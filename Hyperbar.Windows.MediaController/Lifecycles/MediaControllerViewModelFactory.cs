﻿namespace Hyperbar.Windows.MediaController;

public class MediaControllerViewModelFactory(IServiceFactory service) :
    IFactory<MediaController, MediaControllerViewModel?>
{
    public MediaControllerViewModel? Create(MediaController value)
    {
        if (service.Create<MediaControllerViewModel>() 
            is MediaControllerViewModel widgetComponentViewModel)
        {
            return widgetComponentViewModel;
        }

        return default;
    }
}
