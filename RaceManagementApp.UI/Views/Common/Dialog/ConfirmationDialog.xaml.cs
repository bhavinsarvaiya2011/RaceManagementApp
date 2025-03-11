using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RaceManagementApp.UI.Views.Common.Dialog
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ConfirmationDialog : ContentDialog
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string PrimaryButtonText { get; set; }
        public string SecondaryButtonText { get; set; }
        public Action<ContentDialogResult> OnDialogClosed { get; set; }

        public ConfirmationDialog(string title, string message, string primaryButtonText, string secondaryButtonText, Action<ContentDialogResult> onDialogClosed)
        {
            this.InitializeComponent();
            Title = title;
            Message = message;
            PrimaryButtonText = primaryButtonText;
            SecondaryButtonText = secondaryButtonText;
            OnDialogClosed = onDialogClosed;

            DataContext = this;
        }

        private void ConfirmationDialog_Closed(ContentDialog sender, ContentDialogClosedEventArgs args)
        {
            OnDialogClosed?.Invoke(args.Result);
        }

        public async Task ShowDialogAsync(XamlRoot xamlRoot)
        {
            this.XamlRoot = xamlRoot;
            this.Closed += ConfirmationDialog_Closed;
            await this.ShowAsync();
        }
    }
}
