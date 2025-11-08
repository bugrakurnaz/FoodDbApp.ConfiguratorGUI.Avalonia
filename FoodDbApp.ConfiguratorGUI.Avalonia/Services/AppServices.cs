using System;
using Microsoft.Extensions.DependencyInjection;

namespace FoodDbApp.ConfiguratorGUI.Avalonia.Services;

public static class AppServices
{
    public static IServiceProvider? Services { get; private set; }

    public static void ConfigureServices(IServiceCollection serviceCollection)
    {
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