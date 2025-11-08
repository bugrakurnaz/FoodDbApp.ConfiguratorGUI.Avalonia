using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;
using FoodDbApp.ConfiguratorGUI.Avalonia.Primitives;

namespace FoodDbApp.ConfiguratorGUI.Avalonia.Converters;

public class DatabaseItemTypeToStringConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not DatabaseItemType dbItemType)
        {
            return BindingOperations.DoNothing;
        }

        return dbItemType switch
        {
            DatabaseItemType.Category => "Categories",
            DatabaseItemType.StorageLocation => "Storage Locations",
            DatabaseItemType.InventoryItem => "Inventory Items",
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return BindingOperations.DoNothing;
    }
}