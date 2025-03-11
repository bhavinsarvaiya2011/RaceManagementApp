using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceManagementApp.Business.Models
{
    public class UserActionLog
    {
        [Key]
        public long LogId { get; set; }
        public long? UserId { get; set; }
        public string ActionName { get; set; }
        public DateTime ActionTimestamp { get; set; } = DateTime.Now;

        public UserMaster User { get; set; } // Navigation property
    }
}
