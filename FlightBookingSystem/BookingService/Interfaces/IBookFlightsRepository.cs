using BookingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.Interfaces
{
    public interface IBookFlightsRepository
    {
        public string BookFlights(BookingInputDetails[] bookingInputDetails);

        public bool CancelBooking(int PNR);
    }
}
