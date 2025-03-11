using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceManagementApp.Business.Models
{
    public class SystemActionLog
    {
        [Key]
        public long LogId { get; set; }
        public string EventName { get; set; }
        public string EventData { get; set; }
        public DateTime EventTimestamp { get; set; } = DateTime.Now;
    }
}
