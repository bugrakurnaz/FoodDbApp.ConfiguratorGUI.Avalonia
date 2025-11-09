using Avalonia.Controls.Notifications;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.Messaging;
using FoodDbApp.ConfiguratorGUI.Avalonia.Interfaces;

namespace FoodDbApp.ConfiguratorGUI.Avalonia.Services;

public class NotificationSenderService : INotificationSenderService
{
    public void SendNotification(string title, string message, NotificationType notificationType)
    {
        WeakReferenceMessenger.Default.Send(new Notification(title, message, notificationType));
    }
}