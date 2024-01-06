﻿namespace Hyperbar;

public interface INotificationHandler<in TNotification>
    where TNotification :
    INotification
{
    ValueTask Handle(TNotification notification,
        CancellationToken cancellationToken);
}