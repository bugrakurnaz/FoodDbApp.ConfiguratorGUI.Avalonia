using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace FoodDbApp.ConfiguratorGUI.Avalonia.ViewModels.StorageLocations;

public sealed partial class StorageLocationViewModel : ObservableObject
{
    [ObservableProperty] 
    private Guid _id;
    
    [ObservableProperty] 
    private string _name = string.Empty;
    
    [ObservableProperty]
    private string _description = string.Empty;

    public StorageLocationViewModel()
    {
        
    }

    public StorageLocationViewModel(WebClient.Net.Models.StorageLocation storageLocation)
    {
        this.Id = storageLocation.Id;
        this.Name = storageLocation.Name;
        this.Description = storageLocation.Description;
    }

    public WebClient.Net.Models.StorageLocation ToModel()
    {
        return new WebClient.Net.Models.StorageLocation()
        {
            Id = this.Id,
            Name = this.Name,
            Description = this.Description
        };
    }
}