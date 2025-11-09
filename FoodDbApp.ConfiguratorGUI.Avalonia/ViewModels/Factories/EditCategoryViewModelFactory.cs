using FoodDbApp.ConfiguratorGUI.Avalonia.Interfaces;
using FoodDbApp.ConfiguratorGUI.Avalonia.ViewModels.Category;
using FoodDbApp.WebClient.Net.Interfaces;

namespace FoodDbApp.ConfiguratorGUI.Avalonia.ViewModels.Factories;

public class EditCategoryViewModelFactory(ICategoriesApi categoriesApi, INotificationSenderService notificationSenderService) 
    : IEditCategoryViewModelFactory
{
    public EditCategoryViewModel Create(CategoryViewModel categoryViewModel)
    {
        return new EditCategoryViewModel(categoryViewModel, categoriesApi, notificationSenderService);
    }
}