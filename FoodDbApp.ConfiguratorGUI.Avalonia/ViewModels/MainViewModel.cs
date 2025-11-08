using CommunityToolkit.Mvvm.ComponentModel;

namespace FoodDbApp.ConfiguratorGUI.Avalonia.ViewModels;

public sealed partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private NavigationBarViewModel _navigationBarViewModel = new();
    
    [ObservableProperty]
    private NavigatingContainerViewModel _navigatingContainerViewModel = new();
}