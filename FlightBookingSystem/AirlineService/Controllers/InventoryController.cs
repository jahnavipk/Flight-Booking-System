using AirlineService.Models;
using Common;
using CommonDAL.Models;
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
        FlightBookingDBContext _context;

        public InventoryController(FlightBookingDBContext context)
        {
            _context = context;
        }

        [HttpPost("add")]
        public IActionResult AddFlightDetails(FlightMaster inventoryDetails)
        {
            try
            {
                inventoryDetails.IsActive = "Y";
                inventoryDetails.CreatedBy = inventoryDetails.ModifiedBy = "Admin";

                _context.FlightMasters.Add(inventoryDetails);
                _context.SaveChanges();

                return Ok("Flight details added successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(new { Response = "Error", ResponseMessage = ex.Message });
            }
        }
    }
}
