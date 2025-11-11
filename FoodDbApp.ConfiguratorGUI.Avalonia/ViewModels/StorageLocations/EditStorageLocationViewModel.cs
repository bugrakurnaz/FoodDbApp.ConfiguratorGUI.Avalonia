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

public sealed partial class EditStorageLocationViewModel : ObservableObject
{
    private readonly StorageLocationViewModel _storageLocationViewModel;
    private readonly IStorageLocationsApi _storageLocationsApi;
    private readonly INotificationSenderService _notificationSenderService;
    
    [ObservableProperty] 
    private string _name = string.Empty;
    
    [ObservableProperty]
    private string _description = string.Empty;

    partial void OnNameChanged(string value)
    {
        IsEditPossible = !string.IsNullOrWhiteSpace(value);
    }

    [ObservableProperty] 
    private bool _isEditPossible = false;

    public EditStorageLocationViewModel(StorageLocationViewModel storageLocationViewModel, 
        IStorageLocationsApi storageLocationsApi,
        INotificationSenderService notificationSenderService)
    {
        _storageLocationViewModel = storageLocationViewModel;
        _storageLocationsApi = storageLocationsApi;
        _notificationSenderService = notificationSenderService;
        this.Name = storageLocationViewModel.Name;
        this.Description = storageLocationViewModel.Description;
    }

    [RelayCommand]
    private void UpdateStorageLocation()
    {
        Task.Run(UpdateStorageLocationAsync);
    }
    
    private async Task UpdateStorageLocationAsync()
    {
        var storageLocationModel = _storageLocationViewModel.ToModel();
        storageLocationModel.Name = this.Name;
        storageLocationModel.Description = this.Description;
        try
        {
            await _storageLocationsApi.Update(storageLocationModel.Id, storageLocationModel);
            _notificationSenderService.SendNotification("Success", "StorageLocation updated successfully", NotificationType.Success);
            WeakReferenceMessenger.Default.Send(new StorageLocationChangedMessage());
        }
        catch (ApiException apiException)
        {
            _notificationSenderService.SendNotification("Api Error", apiException.ToErrorMessage(),
                NotificationType.Error);
        }
        catch (Exception ex)
        {
            _notificationSenderService.SendNotification("Error", ex.Message, NotificationType.Error);
        }
    }
}