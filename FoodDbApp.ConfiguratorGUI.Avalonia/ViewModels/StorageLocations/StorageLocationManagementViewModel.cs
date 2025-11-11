using CommunityToolkit.Mvvm.ComponentModel;
using FoodDbApp.ConfiguratorGUI.Avalonia.Services;

namespace FoodDbApp.ConfiguratorGUI.Avalonia.ViewModels.StorageLocations;

public sealed partial class StorageLocationManagementViewModel
    : ObservableObject
{
    public AllStorageLocationsViewModel AllStorageLocationsViewModel => AppServices.GetService<StorageLocations.AllStorageLocationsViewModel>();

    public AddStorageLocationViewModel AddStorageLocationViewModel => AppServices.GetService<AddStorageLocationViewModel>();
}