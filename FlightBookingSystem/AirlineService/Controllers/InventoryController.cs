using AirlineService.Interfaces;
using AirlineService.Models;
using Common;
using CommonDAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineService.Controllers
{
    [Route("api/airline/inventory")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        IAirlineRepository _context;

        public InventoryController(IAirlineRepository context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost("add")]
        public IActionResult AddFlightDetails(FlightMaster[] inventoryDetails)
        {
            try
            {
                int isFlightAddedSuccessfully = _context.AddFlightDetails(inventoryDetails);

                if (isFlightAddedSuccessfully > 0)
                {
                    return Ok(new { Response = "Flight details added successfully" });
                }
                else
                {
                    return BadRequest("Flight details could not be added");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Response = "Error", ResponseMessage = ex.Message });
            }
        }
    }
}
