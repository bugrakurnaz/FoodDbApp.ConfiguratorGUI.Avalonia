using System;
using System.Threading.Tasks;
using Avalonia.Controls.Notifications;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FoodDbApp.ConfiguratorGUI.Avalonia.Messages;
using FoodDbApp.WebClient.Net.Interfaces;
using Refit;

namespace FoodDbApp.ConfiguratorGUI.Avalonia.ViewModels.Category;

public sealed partial class EditCategoryViewModel : ObservableObject
{
    [ObservableProperty] 
    private string _name = string.Empty;

    partial void OnNameChanged(string value)
    {
        IsEditPossible = !string.IsNullOrWhiteSpace(value);
    }

    [ObservableProperty] 
    private bool _isEditPossible = false;

    private readonly CategoryViewModel _categoryViewModel;
    private readonly ICategoriesApi _categoriesApi;

    public EditCategoryViewModel(CategoryViewModel categoryViewModel, ICategoriesApi categoriesApi)
    {
        _categoryViewModel = categoryViewModel;
        _categoriesApi = categoriesApi;
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
        }
        catch (ApiException apiException)
        {
            WeakReferenceMessenger.Default.Send(new NotificationMessage()
            {
                Notification = new Notification()
                {
                    Title = "Error",
                    Message = $"""
                               An error occured while updating category: {apiException.Message}""
                               HttpMethod: {apiException.HttpMethod}
                               Path: {apiException.Uri?.AbsolutePath ?? string.Empty}
                               StatusCode: {apiException.StatusCode}
                               """,
                    Type = NotificationType.Error
                }
            });
        }
    }
}