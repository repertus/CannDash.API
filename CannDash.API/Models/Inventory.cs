using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CannDash.API.Models
{
    public class Inventory
    {
        public int InventoryId { get; set; }
        public int? DispensaryId { get; set; }
        public bool Mobile { get; set; }
        public int? Inv_Gram { get; set; }
        public int? Inv_TwoGrams { get; set; }
        public int? Inv_Eigth { get; set; }
        public int? Inv_Quarter { get; set; }
        public int? Inv_HalfOnce { get; set; }
        public int? Inv_Ounce { get; set; }

        //One relationship
        public virtual Dispensary Dispensary { get; set; }

        //Many relationship
        public virtual ICollection<PickUp> Pickups { get; set; }


    }
}