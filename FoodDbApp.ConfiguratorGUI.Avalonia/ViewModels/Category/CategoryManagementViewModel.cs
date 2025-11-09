using System;
using CommunityToolkit.Mvvm.ComponentModel;
using FoodDbApp.ConfiguratorGUI.Avalonia.Interfaces;
using FoodDbApp.ConfiguratorGUI.Avalonia.Services;
using FoodDbApp.WebClient.Net.Interfaces;

namespace FoodDbApp.ConfiguratorGUI.Avalonia.ViewModels.Category;

public sealed partial class CategoryManagementViewModel
    : ObservableObject
{
    public AllCategoriesViewModel AllCategoriesViewModel => AppServices.GetService<AllCategoriesViewModel>();

    public AddCategoryViewModel AddCategoryViewModel => AppServices.GetService<AddCategoryViewModel>();
}