using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using FoodDbApp.ConfiguratorGUI.Avalonia.Interfaces;
using FoodDbApp.ConfiguratorGUI.Avalonia.Messages;
using FoodDbApp.ConfiguratorGUI.Avalonia.Primitives;
using FoodDbApp.ConfiguratorGUI.Avalonia.Services;
using FoodDbApp.ConfiguratorGUI.Avalonia.ViewModels.Category;
using FoodDbApp.WebClient.Net.Interfaces;

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
        CurrentlyDisplayedViewModel = AppServices.GetService<CategoryManagementViewModel>();
    }

    public void Receive(SelectedDatabaseItemTypeChangedMessage message)
    {
        CurrentlyDisplayedViewModel = message.ItemType switch
        {
            DatabaseItemType.Category => AppServices.GetService<CategoryManagementViewModel>(),
            DatabaseItemType.StorageLocation => null,
            DatabaseItemType.InventoryItem => null,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}