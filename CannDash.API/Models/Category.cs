using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CannDash.API.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public int? DispensaryId { get; set; }

        public string CategoryName { get; set; }

        //Many relationship
        public virtual ICollection<Product> Products { get; set; }
    }
}