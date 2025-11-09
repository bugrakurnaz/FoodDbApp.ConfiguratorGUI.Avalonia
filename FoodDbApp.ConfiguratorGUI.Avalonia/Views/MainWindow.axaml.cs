using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.Messaging;
using FoodDbApp.ConfiguratorGUI.Avalonia.Messages;
// ReSharper disable RedundantTypeArgumentsOfMethod

namespace FoodDbApp.ConfiguratorGUI.Avalonia.Views;

public partial class MainWindow : Window, IRecipient<Notification>
{
    public MainWindow()
    {
        InitializeComponent();
        WeakReferenceMessenger.Default.Register<Notification>(this);
    }

    public void Receive(Notification message)
    {
        Dispatcher.UIThread.InvokeAsync(() =>
        {
            WindowNotificationManager.Show(message);
        });
    }
}