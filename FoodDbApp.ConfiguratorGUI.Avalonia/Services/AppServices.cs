using System;
using FoodDbApp.ConfiguratorGUI.Avalonia.Interfaces;
using FoodDbApp.ConfiguratorGUI.Avalonia.ViewModels;
using FoodDbApp.ConfiguratorGUI.Avalonia.ViewModels.Category;
using FoodDbApp.ConfiguratorGUI.Avalonia.ViewModels.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace FoodDbApp.ConfiguratorGUI.Avalonia.Services;

public static class AppServices
{
    private static IServiceProvider? Services { get; set; }

    public static void ConfigureServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<INotificationSenderService, NotificationSenderService>();
        serviceCollection.AddSingleton<MainViewModel>();
        serviceCollection.AddSingleton<NavigationBarViewModel>();
        serviceCollection.AddSingleton<NavigatingContainerViewModel>();
        
        // Categories
        serviceCollection.AddSingleton<IEditCategoryViewModelFactory, EditCategoryViewModelFactory>();
        serviceCollection.AddTransient<AddCategoryViewModel>();
        serviceCollection.AddTransient<AllCategoriesViewModel>();
        serviceCollection.AddTransient<CategoryManagementViewModel>();
    }
    
    public static void SetProvider(IServiceProvider serviceProvider)
    {
        Services = serviceProvider;
    }

    public static T GetService<T>() where T : notnull
    {
        return Services!.GetRequiredService<T>();
    }
}