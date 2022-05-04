using CommonDAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketService.Models;

namespace TicketService.Controllers
{
    [Route("api/ticket")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        FlightBookingDBContext _context;

        public TicketController(FlightBookingDBContext context)
        {
            _context = context;
        }

        [HttpGet("{pnr}")]
        public IActionResult GetBookingDetails(int pnr)
        {
            try
            {
                BookingDetail details = _context.BookingDetails.ToList().Find(m => m.Pnr == pnr);

                if (details != null)
                {
                    IEnumerable<BookingDetail> bookingDetails = _context.BookingDetails.ToList().Where(m => m.Pnr == pnr);
                    IEnumerable<UserBookingDetail> userBookingDetails = _context.UserBookingDetails.ToList().Where(m => m.Pnr == pnr);
                    IEnumerable<UserMaster> userMaster = _context.UserMasters.ToList().Where(m => m.UserId == bookingDetails.FirstOrDefault().UserId);

                    var result = (from p in userBookingDetails
                                  join t in bookingDetails on p.Pnr equals t.Pnr
                                  join c in userMaster on t.UserId equals c.UserId
                                  where t.Pnr == pnr && t.StatusCode == 1
                                  select new
                                  {
                                      t.Pnr,
                                      c.UserName,
                                      t.FlightNo,
                                      p.PassengerName,
                                      p.PassengerAge,
                                      p.PassengerGender,
                                      p.IsMealOpted,
                                      p.MealType,
                                      t.DepartureDateTime,
                                      t.IsOneWay,
                                      t.ReturnDateTime,
                                      t.NoOfPassengers,
                                      p.Price,
                                      p.StatusCode
                                  }).ToList();
                    return Ok(result);
                }

                return NotFound("No records found with the entered PNR number. Please enter the correct PNR number.");
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
