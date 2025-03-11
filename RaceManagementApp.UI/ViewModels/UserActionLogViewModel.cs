using RaceManagementApp.Business.Models;
using RaceManagementApp.UI.Services;
using RaceManagementApp.UI.Utility.Logger;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace RaceManagementApp.UI.ViewModels
{
    public class UserActionLogViewModel: BaseLogger
    {
        private readonly UserActionLogService _userActionLogService;

        public ObservableCollection<UserActionLog> UserActionLog { get; set; } = new ObservableCollection<UserActionLog>();

        public UserActionLogViewModel(UserActionLogService userService, ILoggerService<BaseLogger> logger) : base(logger)
        {
            _userActionLogService = userService;
            LoadUserActionLogAsync();
        }
        public async Task LoadUserActionLogAsync()
        {
            try
            {
                LogInfo("Load User Action Logs....");
                var ListUserActionLog = await _userActionLogService.GetUserActionAsync();
                UserActionLog.Clear();
                foreach (var userActionLog in ListUserActionLog)
                {
                    UserActionLog.Add(userActionLog);
                }
            }
            catch (Exception ex)
            {
                LogError("failed to load UserActionLogs: ", ex);
                throw;
            }
        }
    }
}
