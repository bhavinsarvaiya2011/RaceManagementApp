using CommunityToolkit.Mvvm.Messaging;
using RaceManagementApp.UI.Helpers;
using RaceManagementApp.UI.Views.Common.Enum;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection.Metadata;
using RaceManagementApp.UI.Utility.Logger;

namespace RaceManagementApp.UI.Services
{
    public class NavigationService : BaseLogger
    {
        public NavigationService(ILoggerService<BaseLogger> logger) : base(logger) // Pass logger to BaseLogger
        {
        }
        public void NavigateToPage(Type pageType, string pageTag, string header, string content, object parameter = null)
        {
            LogInfo($"Navigating to page: {pageType.Name}");
            WeakReferenceMessenger.Default.Send(new NavigateMessageHelper(pageType, pageTag, header, content, parameter));
        }
        public void GoBack(bool canGoBack)
        {
            WeakReferenceMessenger.Default.Send(new NavigateMessageHelper(canGoBack));
        }
    }
}
