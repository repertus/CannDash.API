using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CannDash.API.Models
{
    public class Dispensary
    {
        //[ForeignKey("User")]
        public int DispensaryId { get; set; }

        public string CompanyName { get; set; }
        public string WeedMapMenu { get; set; }
        public string Street { get; set; }
        public string UnitNo { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        // Get geo-location -> convert to latitude and longitude
        public string Zone { get; set; }
        public string StatePermit { get; set; }
        public DateTime? PermitExpirationDate { get; set; }

        //One relationship
        public virtual User User { get; set; }
        
        //Many relationship
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Driver> Drivers { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}