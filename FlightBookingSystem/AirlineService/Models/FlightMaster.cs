using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineService.Models
{
    public class FlightMaster
    {
        public string FlightNo { get; set; }
        public string FlightName { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public string DepartureDate { get; set; }
        public string ArrivalDate { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public decimal Price { get; set; }
        public int NoOfSeats { get; set; }
        public string MealOption { get; set; }
        public string IsActive { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
