using CommunityToolkit.Mvvm.ComponentModel;

namespace FoodDbApp.ConfiguratorGUI.Avalonia.ViewModels;

public sealed partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private NavigationBarViewModel _navigationBarViewModel;

    [ObservableProperty] 
    private NavigatingContainerViewModel _navigatingContainerViewModel;

    public MainViewModel(NavigationBarViewModel navigationBarViewModel, NavigatingContainerViewModel navigatingContainerViewModel)
    {
        NavigationBarViewModel = navigationBarViewModel;
        NavigatingContainerViewModel = navigatingContainerViewModel;
    }
}