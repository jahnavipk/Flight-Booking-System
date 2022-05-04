using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketService.Models
{
    public class BookingDetails
    {
        public int UserId { get; set; }
        public string BookingId { get; set; }
        public string PNRNo { get; set; }
        public string PassengerName { get; set; }
        public string PassengerGender { get; set; }
        public int PassengerAge { get; set; }
        public string FlightNo { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public decimal Price { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public char IsOneWay { get; set; }
        public DateTime? ReturnDateTime { get; set; }
        public char IsMealOpted { get; set; }
        public string MealType { get; set; }
        public int StatusCode { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
