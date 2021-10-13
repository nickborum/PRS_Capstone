using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PRS_Capstone.Models
{
    public class Vendor{

        public int Id { get; set; }
        // used lambda code through Flui API instead of using attributes here
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public int? Phone { get; set; }
        public string Email { get; set; }

        public virtual IEnumerable<Product> Products { get; set; }

        //public virtual Request Request { get; set; }

        public Vendor() { }
    }
}
