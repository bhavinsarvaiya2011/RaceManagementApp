using RaceManagementApp.UI.Utility.Logger;

namespace RaceManagementApp.UI.Services
{
    public class UICommonService:BaseLogger
    {
        public UICommonService(ILoggerService<BaseLogger> logger) : base(logger) // Pass logger to BaseLogger
        {
        }
        public dynamic GetLoggedInUser()
        {
            return Microsoft.UI.Xaml.Application.Current.Resources["LoggedInUser"] as dynamic;
        }
    }
}
