using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CannDash.API.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductPic { get; set; }
        public string ProductType { get; set; }

        public int? UnitsOfMeasure { get; set; }

        public bool? Discontinued { get; set; }

        //One relationship
        public virtual Category Category { get; set; }
        public virtual Inventory Inventory { get; set; }
        public virtual Price Price { get; set; }

        //Many relationship
        public virtual ICollection<ProductOrder> ProductOrders { get; set; }
        public virtual ICollection<PickUp> PickUps { get; set; }

    }
}