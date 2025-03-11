using RaceManagementApp.Business.Models;
using RaceManagementApp.UI.Services;
using RaceManagementApp.UI.Utility.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceManagementApp.UI.ViewModels
{
    public class LoginViewModel : BaseLogger
    {
        private readonly UserService _userService;

        public LoginViewModel(UserService userService, ILoggerService<BaseLogger> logger) : base(logger) // Pass logger to BaseLogger
        {
            _userService = userService;
        }
        public async Task<UserMaster> LoginAsync(string username, string password)
        {
            LogInfo("User login attempt...");
            // Validate user credentials
            return await _userService.ValidateUserAsync(username, password);
        }
    }
}
