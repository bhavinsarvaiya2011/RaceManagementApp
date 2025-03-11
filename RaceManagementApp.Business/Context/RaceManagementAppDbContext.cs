using Microsoft.EntityFrameworkCore;
using RaceManagementApp.Business.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceManagementApp.Business.Context
{
    public class RaceManagementAppDbContext : DbContext
    {
        public RaceManagementAppDbContext(DbContextOptions<RaceManagementAppDbContext> options) : base(options) { }
        public DbSet<UserMaster> UserMasters { get; set; }
        public DbSet<UserActionLog> UserActionLog { get; set; }
        public DbSet<SystemActionLog> SystemActionLog { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region UserMaster
            modelBuilder.Entity<UserMaster>()
                .HasIndex(u => u.UserName)
                .IsUnique();
            #endregion

            #region UserActionLog
            modelBuilder.Entity<UserActionLog>()
                .HasOne(ual => ual.User) // UserActionLog has one UserMaster
                .WithMany(um => um.UserActionLogs) // UserMaster has many UserActionLogs
                .HasForeignKey(ual => ual.UserId); // Foreign key is UserId
            #endregion

            #region SystemActionLog
            modelBuilder.Entity<SystemActionLog>()
                .HasKey(x => new { x.LogId });
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
