using FoodDbApp.ConfiguratorGUI.Avalonia.Primitives;

namespace FoodDbApp.ConfiguratorGUI.Avalonia.Messages;

public class SelectedDatabaseItemTypeChangedMessage
{
    public required DatabaseItemType ItemType { get; init; }
}