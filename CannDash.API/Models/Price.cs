using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CannDash.API.Models
{
    public class Price
    {
        public int PriceId { get; set; }
        public int? Price_Gram { get; set; }
        public int? Price_TwoGrams { get; set; }
        public int? Price_Eigth { get; set; }
        public int? Price_Quarter { get; set; }
        public int? Price_HalfOnce { get; set; }
        public int? Price_Ounce { get; set; }

        //One relationship
        public virtual Product Product { get; set; }
    }
}