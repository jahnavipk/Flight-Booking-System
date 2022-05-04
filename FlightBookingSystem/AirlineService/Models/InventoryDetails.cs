using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineService.Models
{
    public class InventoryDetails
    {
        public string FlightNo { get; set; }
        public string FlightName { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public decimal Price { get; set; }
        public int NoOfBusinessSeats { get; set; }
        public int NoOfNonBusinessSeats { get; set; }
        public char MealOption { get; set; }
        public char IsActive { get; set; }
    }
}
