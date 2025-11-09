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

namespace FoodDbApp.ConfiguratorGUI.Avalonia.ViewModels.Category;

public sealed partial class AddCategoryViewModel(ICategoriesApi categoriesApi, INotificationSenderService notificationSenderService) 
    : ObservableObject
{
    [ObservableProperty] 
    private string _name = string.Empty;

    partial void OnNameChanged(string value)
    {
        IsAddPossible = !string.IsNullOrWhiteSpace(value);
    }

    [ObservableProperty] 
    private bool _isAddPossible = false;

    [RelayCommand]
    private void AddCategory()
    {
        if (!IsAddPossible)
        {
            return;
        }

        Task.Run(AddCategoryAsync);
    }

    private async Task AddCategoryAsync()
    {
        var category = new WebClient.Net.Models.Category()
        {
            Name = this.Name
        };
        try
        {
            _ = await categoriesApi.Create(category);
            notificationSenderService.SendNotification("Success", "Category created", NotificationType.Success);
            WeakReferenceMessenger.Default.Send(new CategoryChangedMessage());
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