using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using RaceManagementApp.Business.Models;
using RaceManagementApp.UI.Helpers;
using RaceManagementApp.UI.Services;
using RaceManagementApp.UI.Utility.Logger;
using RaceManagementApp.UI.Views;
using RaceManagementApp.UI.Views.Common.Enum;
using System;
using System.Linq;
using Visibility = Microsoft.UI.Xaml.Visibility;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RaceManagementApp.UI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page 
    {
        private readonly UserActionLogService _userActionLogService;
        private readonly UICommonService _uICommonService;
        private readonly long _currentUserId;
        private readonly ILoggerService<MainPage> _looger;
        public UserMaster User { get; set; }
        public Frame ContentFrame => this.MainContentFrame;
        public MainPage()
        {
            this.InitializeComponent();
            _userActionLogService = App.Host.Services.GetRequiredService<UserActionLogService>();
            _uICommonService = App.Host.Services.GetRequiredService<UICommonService>();
            _currentUserId = _uICommonService.GetLoggedInUser()?.UserID;
            
            _looger = App.Host.Services.GetRequiredService<ILoggerService<MainPage>>();

            WeakReferenceMessenger.Default.Register<NavigateMessageHelper>(this, (r, message) =>
            {
                if (message.CanGoBack)
                {
                    GoBack();
                }
                else 
                {
                    NavigatePage(message.TargetPageType, message.Parameter);
                    OpenOrFocusTab(message.TargetPageType.Name, message.Header, message.Parameter);
                }
            });

            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is UserMaster user)
            {
                try
                {
                    // Set the menu visibility based on the user role
                    SetMenuVisibilityBasedUser(user);
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void SetMenuVisibilityBasedUser(UserMaster user)
        {
            SetDefaultCollapsedMenuItems();
            DefineRoleMenuVisibilityMap(user);
        }

        private void SetDefaultCollapsedMenuItems()
        {
            foreach (var menuItem in MainNavigationView.MenuItems)
            {
                if (menuItem is NavigationViewItem navigationItem)
                {
                    navigationItem.Visibility = Visibility.Collapsed;
                }
                else if (menuItem is NavigationViewItemSeparator separator)
                {
                    separator.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void DefineRoleMenuVisibilityMap(UserMaster user)
        {
            
        }

        private void SideMenu_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItemContainer is NavigationViewItem selectedItem)
            {
                // Navigate based on the Tag set in each NavigationViewItem
                string pageTag = selectedItem.Tag.ToString();
                string content = selectedItem.Content.ToString();
                SideMenuNavigateSelectedPage(pageTag, content);
            }
        }
        private object CreatePageParameter(string pageTag, object data) =>
            pageTag switch
            {
                "SuppliersPage" => new PageParameter(PageType.Supplier, data),
                "SupplierDetailPage" => new PageParameter(PageType.Supplier, data),

                "ClientsPage" => new PageParameter(PageType.Client, data),
                "ClientsDetailPage" => new PageParameter(PageType.Client, data),

                "SalesAndEnquiriesPage" => new PageParameter(PageType.Client, data),
                "SalesAndEnquiriesDetailPage" => new PageParameter(PageType.Client, data),

                "PurchaseOrdersPage" => new PageParameter(PageType.Supplier, data),
                "PurchaseOrderDetailPage" => new PageParameter(PageType.Supplier, data),
                _ => null
            };

        private void NavigationView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (MainContentFrame.CanGoBack)
            {
                MainContentFrame.GoBack();
            }
        }

        public void NavigatePage(System.Type pageType, object parameter)
        {
            ContentFrame.Navigate(pageType, parameter);
        }

        public void GoBack()
        {
            if (ContentFrame?.CanGoBack == true)
                ContentFrame.GoBack();
        }

        private void OpenOrFocusTab(string tag, string header, object parameter = null)
        {
            if (!string.IsNullOrEmpty(header) && !string.IsNullOrEmpty(tag))
            {
                var existingTab = MainTabView.TabItems
              .OfType<TabViewItem>()
              .FirstOrDefault(tab => tab.Tag?.ToString() == tag && tab.Header?.ToString() == header);

                if (existingTab != null)
                {
                    existingTab.Header = !string.IsNullOrEmpty(header) ? header : existingTab.Header;
                    existingTab.DataContext = parameter;
                    MainTabView.SelectedItem = existingTab;
                }
                else
                {
                    var newTab = new TabViewItem
                    {
                        Header = header,
                        Tag = tag,
                        //Content = parameter,
                        DataContext = parameter
                    };

                    MainTabView.TabItems.Add(newTab);
                    MainTabView.SelectedItem = newTab;
                }
            }
        }

        private void MainTabView_TabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
        {
            int removedIndex = sender.TabItems.IndexOf(args.Tab);

            MainTabView.SelectionChanged -= MainTabView_SelectionChanged;
            sender.TabItems.Remove(args.Tab);
            MainTabView.SelectionChanged += MainTabView_SelectionChanged;

            if (sender.TabItems.Count > 0)
            {
                if (removedIndex >= sender.TabItems.Count)
                {
                    removedIndex = sender.TabItems.Count - 1;
                }
                sender.SelectedItem = sender.TabItems[removedIndex];
               
                DispatcherQueue.TryEnqueue(() =>
                {
                    if (sender.ContainerFromItem(sender.SelectedItem) is TabViewItem selectedTab)
                    {
                        foreach (var item in MainNavigationView.MenuItems)
                        {
                            if (item is NavigationViewItem navItem && navItem.Tag is string tag && tag == selectedTab.Tag.ToString())
                            {
                                navItem.IsSelected = true;
                                navItem.Focus(FocusState.Programmatic);
                                break;
                            }
                        }
                    }
                });
            }
        }

        private void MainTabView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainTabView.SelectedItem is TabViewItem selectedTab)
            {
                string pageTag = selectedTab.Tag as string;
                string header = selectedTab.Header?.ToString();
                object content = selectedTab.Content;
                object dataContext = selectedTab.DataContext;

                TabNavigateSelectedPage(pageTag, header, dataContext);
            }
        }


        public void SideMenuNavigateSelectedPage(string pageTag, string header)
        {
            Type pageType = GetPageType(pageTag);

            if (pageType != null)
            {
                object parameter = CreatePageParameter(pageTag, null);
                NavigatePage(pageType, parameter);

                MainTabView.SelectionChanged -= MainTabView_SelectionChanged;
                OpenOrFocusTab(pageTag, header);
                MainTabView.SelectionChanged += MainTabView_SelectionChanged;
                _userActionLogService.AddLogUserActionAsync(_currentUserId, "User are navigation to :" + pageTag);
                _looger.LogInfo("User are navigation to: " + pageTag);


            }
        }

        public void TabNavigateSelectedPage(string pageTag, string header, object parameter)
        {
            Type pageType = GetPageType(pageTag);

            if (pageType != null)
            {
                if (parameter == null)
                {
                    parameter = CreatePageParameter(pageTag, null);
                }

                NavigatePage(pageType, parameter);

                OpenOrFocusTab(pageTag, header, parameter);
            }
        }

        private static Type GetPageType(string pageTag)
        {
            System.Type pageType = pageTag switch
            {
                "DashboardPage" => typeof(DashboardPage),
                "UserPage" => typeof(UserPage),
                "ActivityPage" => typeof(ActivityPage),
                "UserActivityPage" => typeof(UserActivityPage),
                "SystemActivityPage" => typeof(SystemActivityPage),
            };
            return pageType;
        }
    }
}
