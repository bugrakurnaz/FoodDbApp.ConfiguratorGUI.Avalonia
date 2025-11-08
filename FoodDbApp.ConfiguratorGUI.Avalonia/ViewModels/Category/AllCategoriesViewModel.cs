using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls.Notifications;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using FoodDbApp.ConfiguratorGUI.Avalonia.Messages;
using FoodDbApp.WebClient.Net.Interfaces;
using Refit;

namespace FoodDbApp.ConfiguratorGUI.Avalonia.ViewModels.Category;

public sealed partial class AllCategoriesViewModel : ObservableObject
{
    private readonly ICategoriesApi _categoriesApi;
    
    [ObservableProperty]
    private ObservableCollection<CategoryViewModel> _categories = [];
    
    [ObservableProperty]
    private CategoryViewModel? _selectedCategory;

    partial void OnSelectedCategoryChanged(CategoryViewModel? value)
    {
        if (value is null)
        {
            EditCategoryViewModel = null;
            return;
        }

        EditCategoryViewModel = new EditCategoryViewModel(value, _categoriesApi);
    }

    [ObservableProperty] 
    private EditCategoryViewModel? _editCategoryViewModel;

    public AllCategoriesViewModel(ICategoriesApi categoriesApi)
    {
        _categoriesApi = categoriesApi;
        Task.Run(RefreshAllCategoriesAsync);
    }

    private async Task? RefreshAllCategoriesAsync()
    {
        IEnumerable<WebClient.Net.Models.Category> categories;
        try
        {
            categories = await _categoriesApi.GetAll();
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
            return;
        }
        catch (Exception exception)
        {
            WeakReferenceMessenger.Default.Send(new NotificationMessage()
            {
                Notification = new Notification()
                {
                    Title = "Error",
                    Message = $"Undefined Exception: {exception.Message}",
                    Type = NotificationType.Error
                }
            });
            return;
        }
        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            Categories =
                new ObservableCollection<CategoryViewModel>(categories.Select(category => new CategoryViewModel(category)));
        });
    }
}