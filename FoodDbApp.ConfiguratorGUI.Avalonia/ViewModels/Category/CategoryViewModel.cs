using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace FoodDbApp.ConfiguratorGUI.Avalonia.ViewModels.Category;

public sealed partial class CategoryViewModel : ObservableObject
{
    [ObservableProperty] 
    private Guid _id;
    
    [ObservableProperty] 
    private string _name = string.Empty;

    public CategoryViewModel()
    {
        
    }

    public CategoryViewModel(WebClient.Net.Models.Category category)
    {
        this.Id = category.Id;
        this.Name = category.Name;
    } 
}