using System;
using System.Threading.Tasks;
using Avalonia.Controls.Notifications;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FoodDbApp.ConfiguratorGUI.Avalonia.Extensions;
using FoodDbApp.ConfiguratorGUI.Avalonia.Interfaces;
using FoodDbApp.ConfiguratorGUI.Avalonia.Messages;
using FoodDbApp.WebClient.Net.Interfaces;
using Refit;

namespace FoodDbApp.ConfiguratorGUI.Avalonia.ViewModels.StorageLocations;

public sealed partial class AddStorageLocationViewModel(IStorageLocationsApi storageLocationsApi, INotificationSenderService notificationSenderService) 
    : ObservableObject
{
    [ObservableProperty] 
    private string _name = string.Empty;
    
    [ObservableProperty]
    private string _description = string.Empty;

    partial void OnNameChanged(string value)
    {
        IsAddPossible = !string.IsNullOrWhiteSpace(value);
    }

    [ObservableProperty] 
    private bool _isAddPossible = false;

    [RelayCommand]
    private void AddStorageLocation()
    {
        if (!IsAddPossible)
        {
            return;
        }

        Task.Run(AddStorageLocationAsync);
    }

    private async Task AddStorageLocationAsync()
    {
        var storageLocation = new WebClient.Net.Models.StorageLocation()
        {
            Name = this.Name,
            Description = this.Description
        };
        try
        {
            _ = await storageLocationsApi.Create(storageLocation);
            notificationSenderService.SendNotification("Success", "StorageLocation created", NotificationType.Success);
            WeakReferenceMessenger.Default.Send(new StorageLocationChangedMessage());
        }
        catch (ApiException apiException)
        {
            notificationSenderService.SendNotification("Api Error", apiException.ToErrorMessage(),
                NotificationType.Error);
        }
        catch (Exception ex)
        {
            notificationSenderService.SendNotification("Error", ex.Message, NotificationType.Error);
        }
    }
}