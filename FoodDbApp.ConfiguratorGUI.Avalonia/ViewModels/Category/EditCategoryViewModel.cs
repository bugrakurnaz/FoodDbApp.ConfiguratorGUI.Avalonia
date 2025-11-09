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

public sealed partial class EditCategoryViewModel : ObservableObject
{
    private readonly CategoryViewModel _categoryViewModel;
    private readonly ICategoriesApi _categoriesApi;
    private readonly INotificationSenderService _notificationSenderService;
    
    [ObservableProperty] 
    private string _name = string.Empty;

    partial void OnNameChanged(string value)
    {
        IsEditPossible = !string.IsNullOrWhiteSpace(value);
    }

    [ObservableProperty] 
    private bool _isEditPossible = false;

    public EditCategoryViewModel(CategoryViewModel categoryViewModel, 
        ICategoriesApi categoriesApi,
        INotificationSenderService notificationSenderService)
    {
        _categoryViewModel = categoryViewModel;
        _categoriesApi = categoriesApi;
        _notificationSenderService = notificationSenderService;
        this.Name = categoryViewModel.Name;
    }

    [RelayCommand]
    private void UpdateCategory()
    {
        Task.Run(UpdateCategoryAsync);
    }
    
    private async Task UpdateCategoryAsync()
    {
        var categoryModel = _categoryViewModel.ToModel();
        categoryModel.Name = this.Name;
        try
        {
            await _categoriesApi.Update(categoryModel.Id, categoryModel);
            _notificationSenderService.SendNotification("Success", "Category updated successfully", NotificationType.Success);
            WeakReferenceMessenger.Default.Send(new CategoryChangedMessage());
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