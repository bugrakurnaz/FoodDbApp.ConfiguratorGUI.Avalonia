using Avalonia.Controls.Notifications;

namespace FoodDbApp.ConfiguratorGUI.Avalonia.Messages;

public class NotificationMessage
{
    public required INotification Notification { get; init; }
}