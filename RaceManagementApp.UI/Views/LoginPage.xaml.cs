using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using RaceManagementApp.Business.Models;
using RaceManagementApp.UI.Helpers;
using RaceManagementApp.UI.Services;
using RaceManagementApp.UI.ViewModels;
using System;
using System.Threading.Tasks;
using Windows.System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RaceManagementApp.UI.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        private readonly LoginViewModel _viewModel;
        private readonly LoaderService _loaderService;
        private readonly UserActionLogService _userActionLogService;

        public LoginPage()
        {
            this.InitializeComponent();
            _viewModel = App.Host.Services.GetRequiredService<LoginViewModel>();
            _loaderService = App.Host.Services.GetRequiredService<LoaderService>();
            _userActionLogService = App.Host.Services.GetRequiredService<UserActionLogService>();
            UsernameTextBox.Text = "bhavin";
            PasswordBox.Password = "Bhavin@#$8022";
        }
        private void InputBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                LoginButton_Click(sender, e);
            }
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var username = UsernameTextBox.Text;
                var password = PasswordBox.Password;

                _loaderService.IsLoading = true;
                // Validate user and get detail
                var user = await AuthenticateUser(username, password);
                
                if (user != null)
                {
                    Application.Current.Resources["LoggedInUser"] = new
                    {
                        UserID = user.UserID,
                        Username = user.UserName,
                        LoginTime = DateTime.Now
                    };
                    _userActionLogService.AddLogUserActionAsync(user.UserID, "User login attempt");

                    var mainWin = App.Host.Services.GetRequiredService<MainWindow>();
                    _loaderService.IsLoading = false;
                    mainWin.RootFrame.Navigate(typeof(MainPage));
                }
                else
                {
                    _loaderService.IsLoading = false;
                    MessageDialogHelper.ShowErrorMessageAsync(this.XamlRoot, "Invalid username or password. Please try again.");
                }
            }
            catch (System.Exception ex)
            {
                _loaderService.IsLoading = false;
                MessageDialogHelper.ShowErrorMessageAsync(this.XamlRoot, ex.Message);
            }
            finally
            {
                _loaderService.IsLoading = false;
            }
        }
        private async Task<UserMaster> AuthenticateUser(string username, string password)
        {
            return await _viewModel.LoginAsync(username, password);
        }
    }
}
