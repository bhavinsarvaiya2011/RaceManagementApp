using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using RaceManagementApp.UI.ViewModels;
using System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RaceManagementApp.UI.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ActivityPage : Page
    {
        private readonly LogViewModel _viewModel;
        public ActivityPage()
        {
            this.InitializeComponent();
            _viewModel = App.Host.Services.GetRequiredService<LogViewModel>();
            DataContext = _viewModel;
        }
    }
}
