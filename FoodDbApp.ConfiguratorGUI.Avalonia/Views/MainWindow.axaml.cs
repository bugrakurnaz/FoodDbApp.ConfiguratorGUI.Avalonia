using Avalonia.Controls;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.Messaging;
using FoodDbApp.ConfiguratorGUI.Avalonia.Messages;
// ReSharper disable RedundantTypeArgumentsOfMethod

namespace FoodDbApp.ConfiguratorGUI.Avalonia.Views;

public partial class MainWindow : Window, IRecipient<NotificationMessage>
{
    public MainWindow()
    {
        InitializeComponent();
        WeakReferenceMessenger.Default.Register<NotificationMessage>(this);
    }

    public void Receive(NotificationMessage message)
    {
        Dispatcher.UIThread.InvokeAsync(() =>
        {
            WindowNotificationManager.Show(message.Notification);
        });
    }
}