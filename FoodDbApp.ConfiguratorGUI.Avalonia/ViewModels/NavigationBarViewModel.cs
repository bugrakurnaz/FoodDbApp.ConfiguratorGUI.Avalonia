using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using FoodDbApp.ConfiguratorGUI.Avalonia.Messages;
using FoodDbApp.ConfiguratorGUI.Avalonia.Primitives;

namespace FoodDbApp.ConfiguratorGUI.Avalonia.ViewModels;

public sealed partial class NavigationBarViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<DatabaseItemType> _databaseItemTypes =
    [
        DatabaseItemType.Category,
        DatabaseItemType.InventoryItem,
        DatabaseItemType.StorageLocation
    ];

    [ObservableProperty] 
    private DatabaseItemType _selectedDatabaseItemType = DatabaseItemType.Category;

    partial void OnSelectedDatabaseItemTypeChanged(DatabaseItemType value)
    {
        WeakReferenceMessenger.Default.Send(new SelectedDatabaseItemTypeChangedMessage()
        {
            ItemType = value,
        });
    }
}