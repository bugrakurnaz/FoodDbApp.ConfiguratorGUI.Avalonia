using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using FoodDbApp.ConfiguratorGUI.Avalonia.Messages;
using FoodDbApp.ConfiguratorGUI.Avalonia.Primitives;
using FoodDbApp.ConfiguratorGUI.Avalonia.ViewModels.Category;
// ReSharper disable RedundantTypeArgumentsOfMethod

namespace FoodDbApp.ConfiguratorGUI.Avalonia.ViewModels;

public sealed partial class NavigatingContainerViewModel : ObservableObject,
    IRecipient<SelectedDatabaseItemTypeChangedMessage>
{
    [ObservableProperty]
    private ObservableObject? _currentlyDisplayedViewModel;

    public NavigatingContainerViewModel()
    {
        WeakReferenceMessenger.Default.Register<SelectedDatabaseItemTypeChangedMessage>(this);
        CurrentlyDisplayedViewModel = new CategoryManagementViewModel();
    }

    public void Receive(SelectedDatabaseItemTypeChangedMessage message)
    {
        CurrentlyDisplayedViewModel = message.ItemType switch
        {
            DatabaseItemType.Category => new CategoryManagementViewModel(),
            DatabaseItemType.StorageLocation => null,
            DatabaseItemType.InventoryItem => null,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}