namespace Hyperbar.Widget.MediaController.Windows;

public record MediaControllerPlaybackStatus(PlaybackStatus Status) : 
    INotification;
