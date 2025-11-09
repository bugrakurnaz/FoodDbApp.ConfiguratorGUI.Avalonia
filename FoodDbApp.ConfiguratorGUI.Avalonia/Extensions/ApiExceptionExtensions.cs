using Refit;

namespace FoodDbApp.ConfiguratorGUI.Avalonia.Extensions;

public static class ApiExceptionExtensions
{
    public static string ToErrorMessage(this ApiException apiException)
    {
        return $"""
                An error occured while updating category: {apiException.Message}""
                HttpMethod: {apiException.HttpMethod}
                Path: {apiException.Uri?.AbsolutePath ?? string.Empty}
                StatusCode: {apiException.StatusCode}
                """;
    }
}