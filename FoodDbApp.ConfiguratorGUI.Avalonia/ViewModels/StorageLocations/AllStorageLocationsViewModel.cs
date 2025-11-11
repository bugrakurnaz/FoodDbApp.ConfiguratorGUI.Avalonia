using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls.Notifications;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FoodDbApp.ConfiguratorGUI.Avalonia.Extensions;
using FoodDbApp.ConfiguratorGUI.Avalonia.Interfaces;
using FoodDbApp.ConfiguratorGUI.Avalonia.Messages;
using FoodDbApp.WebClient.Net.Interfaces;
using Refit;

namespace FoodDbApp.ConfiguratorGUI.Avalonia.ViewModels.StorageLocations;

public sealed partial class AllStorageLocationsViewModel : ObservableObject, IRecipient<StorageLocationChangedMessage>
{
    private readonly IStorageLocationsApi _storageLocationsApi;
    private readonly INotificationSenderService _notificationSenderService;
    
    [ObservableProperty]
    private ObservableCollection<StorageLocationViewModel> _storageLocations = [];
    
    [ObservableProperty]
    private StorageLocationViewModel? _selectedStorageLocation;

    partial void OnSelectedStorageLocationChanged(StorageLocationViewModel? value)
    {
        if (value is null)
        {
            EditStorageLocationViewModel = null;
            return;
        }

        EditStorageLocationViewModel = new EditStorageLocationViewModel(value, _storageLocationsApi, _notificationSenderService);
    }

    [ObservableProperty] 
    private EditStorageLocationViewModel? _editStorageLocationViewModel;

    public AllStorageLocationsViewModel(IStorageLocationsApi storageLocationsApi, INotificationSenderService notificationSenderService)
    {
        _storageLocationsApi = storageLocationsApi;
        _notificationSenderService = notificationSenderService;
        
        WeakReferenceMessenger.Default.Register<StorageLocationChangedMessage>(this);
        
        Task.Run(RefreshAllStorageLocationsAsync);
    }

    [RelayCommand]
    private void DeleteStorageLocation(StorageLocationViewModel? storageLocation)
    {
        if (storageLocation is null)
        {
            return;
        }
        Task.Run(async () => await DeleteStorageLocationAsync(storageLocation));
    }

    private async Task RefreshAllStorageLocationsAsync()
    {
        IEnumerable<WebClient.Net.Models.StorageLocation> storageLocations;
        try
        {
            storageLocations = await _storageLocationsApi.GetAll();
        }
        catch (ApiException apiException)
        {
            _notificationSenderService.SendNotification("Api Error", apiException.ToErrorMessage(), NotificationType.Error);
            return;
        }
        catch (Exception exception)
        {
            _notificationSenderService.SendNotification("Undefined Error", exception.Message, NotificationType.Error);
            return;
        }
        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            StorageLocations =
                new ObservableCollection<StorageLocationViewModel>(storageLocations.Select(storageLocation => new StorageLocationViewModel(storageLocation)));
        });
    }

    private async Task DeleteStorageLocationAsync(StorageLocationViewModel storageLocation)
    {
        try
        {
            await _storageLocationsApi.Delete(storageLocation.Id);
            await RefreshAllStorageLocationsAsync();
            _notificationSenderService.SendNotification("Success", "StorageLocation Deleted", NotificationType.Success);
        }
        catch (ApiException apiException)
        {
            _notificationSenderService.SendNotification("Api Error", apiException.ToErrorMessage(), NotificationType.Error);
        }
        catch (Exception exception)
        {
            _notificationSenderService.SendNotification("Undefined Error", exception.Message, NotificationType.Error);
        }
    }

    public void Receive(StorageLocationChangedMessage message)
    {
        Task.Run(async () => await RefreshAllStorageLocationsAsync());
    }
}