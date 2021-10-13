using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PRS_Capstone.Models
{
    public class Request {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Justification { get; set; }
        public string RejectionReason { get; set; }
        public string DeliveryMode { get; set; } = "Pick up";
        public string Status { get; set; } = "New";
        public decimal Total { get; set; }

        public virtual IEnumerable<User> User { get; set; }
        public Request() { }
    }
}
