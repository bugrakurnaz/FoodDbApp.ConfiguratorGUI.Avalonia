using Avalonia.Controls.Notifications;

namespace FoodDbApp.ConfiguratorGUI.Avalonia.Interfaces;

public interface INotificationSenderService
{
    void SendNotification(string title, string message, NotificationType notificationType);
}