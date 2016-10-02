using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CannDash.API.Models
{
    public class ProductOrder
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        public int? OrderQty { get; set; }
        public int? UnitPrice { get; set; }
        public int Discount { get; set; }
        public int Total { get; set; }

        //One relationship
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}