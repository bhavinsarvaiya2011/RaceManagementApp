using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using RaceManagementApp.UI.Services;
using RaceManagementApp.UI.ViewModels;
using System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RaceManagementApp.UI.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SystemActivityPage : Page
    {
        private readonly SystemActionLogViewModel _viewModel;
        private readonly LoaderService _loaderService;

        public SystemActivityPage()
        {
            this.InitializeComponent();
            _loaderService = App.Host.Services.GetRequiredService<LoaderService>();
            _loaderService.IsLoading = true;
            _viewModel = App.Host.Services.GetRequiredService<SystemActionLogViewModel>();
            DataContext = _viewModel;
            _loaderService.IsLoading = false;
        }
    }
}
