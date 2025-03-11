using Microsoft.EntityFrameworkCore;
using RaceManagementApp.Business.Context;
using RaceManagementApp.Business.Models;
using RaceManagementApp.UI.Utility.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceManagementApp.UI.Services
{
    public class UserActionLogService : BaseLogger
    {
        private readonly DbContextOptions<RaceManagementAppDbContext> _dbContextOptions;
        public UserActionLogService(DbContextOptions<RaceManagementAppDbContext> dbContextOptions, ILoggerService<BaseLogger> logger) : base(logger) // Pass logger to BaseLogger)
        {
            _dbContextOptions = dbContextOptions;
        }

        public async Task AddLogUserActionAsync(long? userId, string actionName)
        {
            try
            {
                LogInfo($"Logging user action:${actionName}");
                var log = new UserActionLog
                {
                    UserId = userId,
                    ActionName = actionName
                };

                using (var _context = new RaceManagementAppDbContext(_dbContextOptions))
                {
                    _context.UserActionLog.Add(log);
                    await _context.SaveChangesAsync();
                    LogInfo("User action logged successfully.");
                }
            }
            catch (Exception ex)
            {
                LogError("Failed to log user action.", ex);
                throw;
            }
        }
        public async Task<List<UserActionLog>> GetUserActionAsync()
        {
            try
            {
                LogInfo($"Load User Action");
                using var _context = new RaceManagementAppDbContext(_dbContextOptions);
                LogInfo($"Load User Action Successfully");
                return await _context.UserActionLog.Include(x=>x.User).ToListAsync();
            }
            catch (Exception ex)
            {
                LogError("Failed to load User action.", ex);
                throw;
            }
        }
    }
}
