using CommonDAL.Models;
using SearchService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchService.Models
{
    public class SearchFlightsRepository : ISearchFlightsRepository
    {
        FlightBookingDBContext _context;

        public SearchFlightsRepository(FlightBookingDBContext context)
        {
            _context = context;
        }

        public IEnumerable<TblFlightMaster> SearchFlights(SearchDetails searchDetails)
        {
            IEnumerable<TblFlightMaster> searchResults = _context.TblFlightMasters.ToList()
                                                        .Where(m => m.FromLocation == searchDetails.FromLocation
                                                                 && m.ToLocation == searchDetails.ToLocation);

            return searchResults;
        }
    }
}
