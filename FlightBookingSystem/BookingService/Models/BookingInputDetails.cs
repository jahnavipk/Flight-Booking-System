using CommonDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.Models
{
    public class BookingInputDetails
    {
        public int UserId { get; set; }
        public string FlightNo { get; set; }
        public int NoOfPassengers { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public string IsOneWay { get; set; }
        public DateTime? ReturnDateTime { get; set; }        
        public TblPassengerDetail[] TblPassengerDetails { get; set; }
    }
}
