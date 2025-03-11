using RaceManagementApp.UI.Utility.Logger;
using RaceManagementApp.UI.Services;
using RaceManagementApp.UI.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RaceManagementApp.UI
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private readonly LoaderService _loaderService;
        public Frame RootFrame => this.rootFrame;
        public MainWindow()
        {
            this.InitializeComponent();
            _loaderService = App.Host.Services.GetRequiredService<LoaderService>();
            RootGrid.DataContext = _loaderService;
            SetFullScreen();
            RootFrame.Navigate(typeof(LoginPage));
        }
        private void SetFullScreen()
        {
            // For FullScreen window
            // If wants to open the app in full screen then comment the below code this same commnet found in bottom
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd);
            var appWindow = AppWindow.GetFromWindowId(windowId);
            var displayArea = DisplayArea.GetFromWindowId(windowId, DisplayAreaFallback.Primary);
            var workArea = displayArea.WorkArea;
            appWindow.MoveAndResize(new Windows.Graphics.RectInt32
            {
                X = workArea.X,
                Y = workArea.Y,
                Width = workArea.Width,
                Height = workArea.Height
            });
            // If wants to open the app in full screen then comment the below code this same commnet found in bottom
        }
    }
}
