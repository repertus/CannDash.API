using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CannDash.API.Models
{
    public class User
    {
        public int UserId { get; set; }
        //public int? CustomerId { get; set; }
        //public int? DispensaryId { get; set; }

        public string UserName { get; set; }
        public string PassWord { get; set; }

        //One relationship
        public virtual Customer Customer { get; set; }
        public virtual Dispensary Dispensary { get; set; }

    }
}