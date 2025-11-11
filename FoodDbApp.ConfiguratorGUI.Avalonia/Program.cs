using Avalonia;
using System;
using FoodDbApp.ConfiguratorGUI.Avalonia.Services;
using FoodDbApp.WebClient.Net.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;

namespace FoodDbApp.ConfiguratorGUI.Avalonia;

sealed class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        var host = CreateHost(args);
        AppServices.SetProvider(host.Services); // Store the service provider

        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }
    
    public static IHost CreateHost(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                string baseAddress = "http://localhost:8349";
                services.AddRefitClient<ICategoriesApi>()
                    .ConfigureHttpClient(c =>
                    {
                        c.BaseAddress = new Uri(baseAddress);
                    })
                    .SetHandlerLifetime(TimeSpan.FromSeconds(10));

                services.AddRefitClient<IStorageLocationsApi>()
                    .ConfigureHttpClient(c =>
                    {
                        c.BaseAddress = new Uri(baseAddress);
                    })
                    .SetHandlerLifetime(TimeSpan.FromSeconds(10));

                AppServices.ConfigureServices(services);

            })
            .Build();

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}