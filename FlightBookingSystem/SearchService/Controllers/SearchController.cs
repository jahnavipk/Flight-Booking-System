using CommonDAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SearchService.Interfaces;
using SearchService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Author: Jahnavi Kamatgi
/// Purpose: Search for flights
/// </summary>
namespace SearchService.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {

        ISearchFlightsRepository _context;

        public SearchController(ISearchFlightsRepository context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult SearchFlights(SearchDetails searchDetails)
        {
            try
            {
                var searchResults = _context.SearchFlights(searchDetails);

                if (searchResults.ToList().Count != 0)
                {
                    return Ok(searchResults.ToList());
                }
                else
                {
                    return NotFound(new { Response = "No data found" });
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
