using CommonDAL.Models;
using SearchService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchService.Interfaces
{
    public interface ISearchFlightsRepository
    {
        public IEnumerable<TblFlightMaster> SearchFlights(SearchDetails searchDetails);
    }
}
