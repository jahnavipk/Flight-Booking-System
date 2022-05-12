using CommonDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketService.Interfaces;

namespace TicketService.Models
{
    public class TicketRepository: ITicketRepository
    {
        FlightBookingDBContext _context;

        public TicketRepository(FlightBookingDBContext context)
        {
            _context = context;
        }
    }
}
