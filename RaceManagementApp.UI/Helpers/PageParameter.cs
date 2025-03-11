using RaceManagementApp.UI.Views.Common.Enum;

namespace RaceManagementApp.UI.Helpers
{
    public class PageParameter
    {
        public PageType PageType { get; set; }
        public object Data { get; set; }
        public PageParameter(PageType pageType, object data = null)
        {
            PageType = pageType;
            Data = data;
        }

    }
}
