using AutoMapper;
using CommunityToolkit.Mvvm.Input;
using RaceManagementApp.UI.Services;
using System.Threading.Tasks;

namespace RaceManagementApp.UI.ViewModels
{
    public class LogViewModel : ViewModelBase
    {
        private readonly UserActionLogService _userActionLogService;
        private readonly SystemActionLogService _systemActionLogService;
        private readonly UICommonService _uICommonService;


        private readonly long _currentUserId;
        public IAsyncRelayCommand PreStageCommand { get; }
        public IAsyncRelayCommand StageCommand { get; }
        public IAsyncRelayCommand GreenLightCommand { get; }
        public IAsyncRelayCommand RedLightCommand { get; }
        public IAsyncRelayCommand SensorCommand { get; }
        
        public LogViewModel(UserActionLogService userActionLogService, SystemActionLogService systemActionLogService,UICommonService uICommonService, IMapper mapper) : base(mapper)
        {
            _uICommonService = uICommonService;

            _currentUserId = _uICommonService.GetLoggedInUser()?.UserID;
            _userActionLogService = userActionLogService;
            _systemActionLogService = systemActionLogService;

            PreStageCommand = new AsyncRelayCommand(ExecutePreStage);
            StageCommand = new AsyncRelayCommand(ExecuteStage);
            GreenLightCommand = new AsyncRelayCommand(ExecuteGreenLight);
            RedLightCommand = new AsyncRelayCommand(ExecuteRedLight);
            SensorCommand = new AsyncRelayCommand(ExecuteSensor);

        }
        public async Task ExecutePreStage()
        {
           await _userActionLogService.AddLogUserActionAsync(_currentUserId, "Clicked Pre-Stage Button");
           await _systemActionLogService.AddLogSystemActionAsync("Pre-Stage Button Clicked","Sample Data");
        }

        public async Task ExecuteStage()
        {
            await _userActionLogService.AddLogUserActionAsync(_currentUserId, "Clicked Stage Button");
            await _systemActionLogService.AddLogSystemActionAsync("Stage Button Clicked", "Sample Data");
        }

        public async Task ExecuteGreenLight()
        {
            await _userActionLogService.AddLogUserActionAsync(_currentUserId, "Clicked Green Light Button");
            await _systemActionLogService.AddLogSystemActionAsync("Green Light Activated", "Sample Data");
        }

        public async Task ExecuteRedLight()
        {
            await _userActionLogService.AddLogUserActionAsync(_currentUserId, "Clicked Red Light Button");
            await _systemActionLogService.AddLogSystemActionAsync("Red Light Activated", "Sample Data");
        }

        public async Task ExecuteSensor()
        {
            await _userActionLogService.AddLogUserActionAsync(_currentUserId, "Clicked Sensor Button");
            await _systemActionLogService.AddLogSystemActionAsync("Sensor Data Received", "Sample Data");
        }
    }
}
