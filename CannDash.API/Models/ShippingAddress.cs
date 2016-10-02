using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CannDash.API.Models
{
    public class ShippingAddress
    {
        public int ShippingAddressId { get; set; }
        public int? CustomerId { get; set; }

        public string Street { get; set; }
        public string UnitNo { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int? ZipCode { get; set; }

        //One relationship
        public virtual Customer Customer { get; set; }

        //Many relationship
        public virtual ICollection<Order> Orders { get; set; }
    }
}