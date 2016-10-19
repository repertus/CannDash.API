using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CannDash.API.Models
{
    public class ProductOrder
    {
        public int ProductOrderId { get; set; }
        public int OrderId { get; set; }
        public int? MenuCategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? ProductId { get; set; }
        public string ProductName { get; set; }

        public int? OrderQty { get; set; }
        public int? Price { get; set; }
        public string Units { get; set; }
        public int? Discount { get; set; }
        public int? TotalSale { get; set; }

        //One relationship
        public virtual Order Order { get; set; }
    }
}