using RaceManagementApp.Business.Models;
using RaceManagementApp.UI.Services;
using RaceManagementApp.UI.Utility.Logger;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace RaceManagementApp.UI.ViewModels
{
    public class SystemActionLogViewModel: BaseLogger
    {
        private readonly SystemActionLogService _systemActionLogService;

        public ObservableCollection<SystemActionLog> SystemActionLog { get; set; } = new ObservableCollection<SystemActionLog>();

        public SystemActionLogViewModel(SystemActionLogService systemActionLogService, ILoggerService<BaseLogger> logger) : base(logger) // Pass logger to BaseLogger
        {
            _systemActionLogService = systemActionLogService;
            LoadSystemActionLogAsync();
        }
        public async Task LoadSystemActionLogAsync()
        {
            try
            {
                LogInfo("Fetching system action logs...");
                var ListSystemActionLog = await _systemActionLogService.GetSystemActionLogAsync();
                SystemActionLog.Clear();
                foreach (var systemActionLog in ListSystemActionLog)
                {
                    SystemActionLog.Add(systemActionLog);
                }
            }
            catch (Exception ex)
            {
                LogError("Failed to load LoadSystemActionLog: ", ex);
                throw;
            }
        }
    }
}
