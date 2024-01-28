namespace Hyperbar.Widget.MediaController.Windows;

public record PlaybackInformation(PlaybackStatus Status) : 
    INotification;
