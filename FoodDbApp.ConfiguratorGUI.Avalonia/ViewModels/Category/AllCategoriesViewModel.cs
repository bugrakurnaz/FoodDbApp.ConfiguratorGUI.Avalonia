using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using FoodDbApp.WebClient.Net.Interfaces;

namespace FoodDbApp.ConfiguratorGUI.Avalonia.ViewModels.Category;

public sealed partial class AllCategoriesViewModel : ObservableObject
{
    private readonly ICategoriesApi _categoriesApi;
    
    [ObservableProperty]
    private ObservableCollection<CategoryViewModel> _categories = [];

    public AllCategoriesViewModel(ICategoriesApi categoriesApi)
    {
        _categoriesApi = categoriesApi;
        Task.Run(RefreshAllCategoriesAsync);
    }

    private async Task? RefreshAllCategoriesAsync()
    {
        var categories = await _categoriesApi.GetAll();
        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            Categories =
                new ObservableCollection<CategoryViewModel>(categories.Select(category => new CategoryViewModel(category)));
        });
    }
}