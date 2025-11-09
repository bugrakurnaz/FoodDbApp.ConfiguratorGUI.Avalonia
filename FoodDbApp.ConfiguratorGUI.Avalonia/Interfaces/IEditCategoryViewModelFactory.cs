using FoodDbApp.ConfiguratorGUI.Avalonia.ViewModels.Category;

namespace FoodDbApp.ConfiguratorGUI.Avalonia.Interfaces;

public interface IEditCategoryViewModelFactory
{
    EditCategoryViewModel Create(CategoryViewModel categoryViewModel);
}