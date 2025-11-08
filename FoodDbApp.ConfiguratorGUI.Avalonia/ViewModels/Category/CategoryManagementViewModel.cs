using System;
using CommunityToolkit.Mvvm.ComponentModel;
using FoodDbApp.ConfiguratorGUI.Avalonia.Services;
using FoodDbApp.WebClient.Net.Interfaces;

namespace FoodDbApp.ConfiguratorGUI.Avalonia.ViewModels.Category;

public sealed partial class CategoryManagementViewModel : ObservableObject
{
    private readonly Lazy<AllCategoriesViewModel> _allCategoriesViewModel = new(() =>
        new AllCategoriesViewModel(categoriesApi: AppServices.GetService<ICategoriesApi>()));
    
    public AllCategoriesViewModel AllCategoriesViewModel => this._allCategoriesViewModel.Value;
}