using CommonDAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SearchService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchService.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        FlightBookingDBContext _context;

        public SearchController(FlightBookingDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult SearchFlights(SearchDetails searchDetails)
        {
            try
            {
                IEnumerable<FlightMaster> searchResults = _context.FlightMasters.ToList()
                                                        .Where(m => m.FromLocation == searchDetails.FromLocation
                                                                 && m.ToLocation == searchDetails.ToLocation);
                if (searchResults.ToList().Count != 0)
                {
                    return Ok(searchResults.ToList());
                }
                else
                {
                    return NotFound("No data found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Response = "Error",
                    ResponseMessage = ex.Message
                });
            }
        }
    }
}
