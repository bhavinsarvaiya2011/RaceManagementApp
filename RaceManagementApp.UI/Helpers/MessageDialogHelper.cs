using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;

namespace RaceManagementApp.UI.Helpers
{
    public static class MessageDialogHelper
    {
        public static async Task ShowMessageAsync(XamlRoot xamlRoot, string title, string message)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = title,
                Content = message,
                CloseButtonText = "OK",
                XamlRoot = xamlRoot
            };

            await dialog.ShowAsync();
        }

        public static async Task ShowSuccessMessageAsync(XamlRoot xamlRoot,string message, string title = "Success")
        {
            await ShowMessageAsync(xamlRoot, title, message);
        }

        public static async Task ShowErrorMessageAsync(XamlRoot xamlRoot, string message, string title = "Error")
        {
            await ShowMessageAsync(xamlRoot, title, message);
        }

        public static async Task ShowInfoMessageAsync(XamlRoot xamlRoot, string message, string title = "Information")
        {
            await ShowMessageAsync(xamlRoot, title, message);
        }
    }
}
