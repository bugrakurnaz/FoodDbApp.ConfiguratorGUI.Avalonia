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

namespace FoodDbApp.ConfiguratorGUI.Avalonia.ViewModels.Category;

public sealed partial class AllCategoriesViewModel : ObservableObject, IRecipient<CategoryChangedMessage>
{
    private readonly ICategoriesApi _categoriesApi;
    private readonly INotificationSenderService _notificationSenderService;
    
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

        EditCategoryViewModel = new EditCategoryViewModel(value, _categoriesApi, _notificationSenderService);
    }

    [ObservableProperty] 
    private EditCategoryViewModel? _editCategoryViewModel;

    public AllCategoriesViewModel(ICategoriesApi categoriesApi, INotificationSenderService notificationSenderService)
    {
        _categoriesApi = categoriesApi;
        _notificationSenderService = notificationSenderService;
        
        WeakReferenceMessenger.Default.Register<CategoryChangedMessage>(this);
        
        Task.Run(RefreshAllCategoriesAsync);
    }

    [RelayCommand]
    private void DeleteCategory(CategoryViewModel? category)
    {
        if (category is null)
        {
            return;
        }
        Task.Run(async () => await DeleteCategoryAsync(category));
    }

    private async Task RefreshAllCategoriesAsync()
    {
        IEnumerable<WebClient.Net.Models.Category> categories;
        try
        {
            categories = await _categoriesApi.GetAll();
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
            Categories =
                new ObservableCollection<CategoryViewModel>(categories.Select(category => new CategoryViewModel(category)));
        });
    }

    private async Task DeleteCategoryAsync(CategoryViewModel category)
    {
        try
        {
            await _categoriesApi.Delete(category.Id);
            await RefreshAllCategoriesAsync();
            _notificationSenderService.SendNotification("Success", "Category Deleted", NotificationType.Success);
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

    public void Receive(CategoryChangedMessage message)
    {
        Task.Run(async () => await RefreshAllCategoriesAsync());
    }
}