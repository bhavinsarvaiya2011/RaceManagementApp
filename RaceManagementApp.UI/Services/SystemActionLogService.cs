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
    public class SystemActionLogService : BaseLogger
    {
        private readonly DbContextOptions<RaceManagementAppDbContext> _dbContextOptions;

        public SystemActionLogService(DbContextOptions<RaceManagementAppDbContext> dbContextOptions,ILoggerService<BaseLogger> logger) : base(logger) // Pass logger to BaseLogger
        {
            _dbContextOptions = dbContextOptions;
        }

        public async Task AddLogSystemActionAsync(string eventName, string eventData)
        {
            try
            {
                LogInfo($"Logging system action: {eventName}");
                var log = new SystemActionLog
                {
                    EventName = eventName,
                    EventData = eventData
                };

                using (var _context = new RaceManagementAppDbContext(_dbContextOptions))
                {
                    _context.SystemActionLog.Add(log);
                    await _context.SaveChangesAsync();
                    LogInfo("System action logged successfully.");
                }
            }
            catch (Exception ex)
            {
                LogError("Failed to log system action.", ex);
                throw;
            }
        }
        public async Task<List<SystemActionLog>> GetSystemActionLogAsync()
        {
            try
            {
                LogInfo($"Load system action");
                using var _context = new RaceManagementAppDbContext(_dbContextOptions);
                LogInfo($"Load system action successfully");
                return await _context.SystemActionLog.ToListAsync();
            }
            catch (Exception ex)
            {
                LogError("Failed to load system action.", ex);
                throw;
            }
        }
    }
}
