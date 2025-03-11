using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceManagementApp.Business.Models
{
    public class UserMaster : AuditItemModel
    {
        [Key]
        public long UserID { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public ICollection<UserActionLog> UserActionLogs { get; set; } // Navigation property
    }
}
