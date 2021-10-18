using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PRS_Capstone.Models
{
    public class RequestLine{
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; } = 1;

        
        public virtual Product Product { get; set; }

        [JsonIgnore]
        public virtual Request Request { get; set; }



        //public virtual IEnumerable<User> Users { get; set; }
        //public virtual IEnumerable<Request> Requests { get; set; }
        //public virtual IEnumerable<Vendor> Vendors { get; set; }
        //public virtual IEnumerable<Product> Products { get; set; }

        




    }
}
