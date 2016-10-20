using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CannDash.API.Models
{
    public class Driver
    {
        public int DriverId { get; set; }
        public int DispensaryId { get; set; }
        public bool DriverCheckIn { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string DriverPic { get; set; }
        public string DriversLicense { get; set; }
        public string LicensePlate { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleColor { get; set; }
        public string VehicleInsurance { get; set; }
        // Get geo-location -> convert to latitude and longitude

        //One relationship
        public virtual Dispensary Dispensary { get; set; }

        //Many relationship
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<PickUp> PickUps { get; set; }
    }
}