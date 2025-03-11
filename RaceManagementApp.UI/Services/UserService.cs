using Microsoft.EntityFrameworkCore;
using RaceManagementApp.Business.Context;
using RaceManagementApp.Business.Models;
using RaceManagementApp.UI.Utility.Helper;
using RaceManagementApp.UI.Utility.Logger;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RaceManagementApp.UI.Services
{
    public class UserService : BaseLogger
    {
        private readonly RaceManagementAppDbContext _context;
        public UserService(RaceManagementAppDbContext context, ILoggerService<BaseLogger> logger) : base(logger) // Pass logger to BaseLogger
        {
            _context = context;
        }

        public async Task<List<UserMaster>> GetUsersAsync()
        {
            try
            {
                LogInfo("Load user data...");
                return await _context.UserMasters.ToListAsync();
            }
            catch (Exception ex)
            {
                LogError("Failed to load...", ex);
                throw;
            }
        }

        // Validate user detail with username and password
        public async Task<UserMaster> ValidateUserAsync(string username, string password)
        {
            try
            {
                // Simulate some business logic
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    LogError("Validation failed. Username or password is empty", new Exception("Validation error"));
                    throw new Exception("Username or password cannot be empty");
                }

                var user = await _context.UserMasters.FirstOrDefaultAsync(u => u.UserName == username);

                if (user == null)
                    throw new Exception("User not found.");

                var isVerify = PasswordHasher.VerifyPassword(password, user.PasswordHash);

                if (!isVerify)
                    throw new Exception($"User {username} not validated.");

                LogInfo($"User {username} validated successfully.");
                return user; // Return if a matching user is found
            }
            catch (Exception ex)
            {
                LogError("Error during user validation", ex);
                throw;  // Re-throw the exception
            }
        }
    }
}
