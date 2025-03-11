using System;

namespace RaceManagementApp.UI.Helpers
{
    public class NavigateMessageHelper
    {
        public Type TargetPageType { get; }
        public string TargetPageTag { get; }
        public string Content { get; set; }
        public string Header { get; set; }
        public object Parameter { get; set; }
        public bool CanGoBack { get; set; }

        public NavigateMessageHelper(Type targetPageType, string targetPageTag, string header, string content, object parameter = null)
        {
            TargetPageType = targetPageType;
            TargetPageTag = targetPageTag;
            Parameter = parameter;
            Content = content;
            Header = header;
        }

        public NavigateMessageHelper(bool canGoBack = false)
        {
            CanGoBack = canGoBack;
        }
    }
}
