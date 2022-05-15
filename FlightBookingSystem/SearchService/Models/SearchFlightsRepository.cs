using CommonDAL.Models;
using SearchService.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            //string departureDate = DateTime.ParseExact(searchDetails.DepartureDate, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

            IEnumerable<TblFlightMaster> searchResults = _context.TblFlightMasters.ToList()
                                                        .Where(m => m.FromLocation == searchDetails.FromLocation
                                                                 && m.ToLocation == searchDetails.ToLocation
                                                                 && m.DepartureDateTime.ToString("yyyy-MM-dd") == searchDetails.DepartureDate
                                                                 && m.IsActive == "Y");


           
            return searchResults;
        }
    }
}
