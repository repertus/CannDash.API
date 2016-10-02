using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CannDash.API.Models
{
    public class Customer
    {
        //[ForeignKey("User")]
        public int CustomerId { get; set; }
        public int DispensaryId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }


        public string Street { get; set; }
        public string UnitNo { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int? Age { get; set; }
        public string MedicalReason { get; set; }
        public string DriversLicense { get; set; }
        public string MmicId { get; set; }
        public DateTime? MmicExpiration { get; set; }
        public string DoctorLetter { get; set; }

        //One relationship
        public virtual Dispensary Dispensary { get; set; }
        public virtual User User { get; set; }

        //Many relationship
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<ShippingAddress> ShippingAddresses { get; set; }
    }
}