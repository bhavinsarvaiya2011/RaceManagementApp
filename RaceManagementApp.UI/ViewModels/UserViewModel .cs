using RaceManagementApp.Business.Models;
using RaceManagementApp.UI.Services;
using RaceManagementApp.UI.Utility.Logger;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace RaceManagementApp.UI.ViewModels
{
    public class UserViewModel: BaseLogger
    {
        private readonly UserService _userService;

        public ObservableCollection<UserMaster> Users { get; set; } = new ObservableCollection<UserMaster>();

        public UserViewModel(UserService userService, ILoggerService<BaseLogger> logger) : base(logger)
        {
            _userService = userService;
            LoadUsersAsync();
        }

        public async Task LoadUsersAsync()
        {
            try
            {
                LogInfo("Load User data");
                var users = await _userService.GetUsersAsync();
                Users.Clear();
                foreach (var user in users)
                {
                    user.CreatedOn = user.CreatedOn;
                    Users.Add(user);
                }
            }
            catch (Exception ex)
            {
                LogError("Failed to load User data: ", ex);
                throw;
            }
        }
    }
}
