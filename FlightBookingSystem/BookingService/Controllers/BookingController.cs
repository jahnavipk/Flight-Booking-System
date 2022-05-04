using BookingService.Models;
using CommonDAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.Controllers
{
    [Route("api/booking")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        FlightBookingDBContext _context;

        public BookingController(FlightBookingDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult BookFlight(BookingInputDetails bookingInputDetails)
        {
            try
            {
                //Should write code here to check if the user is logged in


                //Insert data into Booking Details table
                BookingDetail bookingDetail = new BookingDetail();

                bookingDetail.UserId = bookingInputDetails.UserId;
                bookingDetail.FlightNo = bookingInputDetails.FlightNo;
                bookingDetail.NoOfPassengers = bookingInputDetails.NoOfPassengers;
                bookingDetail.DepartureDateTime = bookingInputDetails.DepartureDateTime;
                bookingDetail.IsOneWay = bookingInputDetails.IsOneWay;
                bookingDetail.ReturnDateTime = bookingInputDetails.ReturnDateTime;
                bookingDetail.StatusCode = 1;
                bookingDetail.CreatedBy = bookingDetail.ModifiedBy = bookingInputDetails.UserId.ToString();

                _context.BookingDetails.Add(bookingDetail);
                _context.SaveChanges();

                //Insert data into UserBookingDetails table (Includes passenger wise details)
                foreach (var item in bookingInputDetails.UserBookingDetails)
                {
                    item.Pnr = bookingDetail.Pnr;
                    item.StatusCode = 1;
                    item.CreatedBy = item.ModifiedBy = bookingInputDetails.UserId.ToString();

                    _context.UserBookingDetails.Add(item);
                    _context.SaveChanges();
                }

                return Ok("Flight Booked Successfully with PNR No: " + bookingDetail.Pnr);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Response = "Error", ResponseMessage = ex.Message });
            }
        }

        [HttpGet("history/{emailId}")]
        public IActionResult GetBookingHistory(string emailId)
        {
            try
            {
                IEnumerable<UserMaster> userMaster = _context.UserMasters.ToList().Where(m => m.EmailId == emailId);
                IEnumerable<BookingDetail> bookingDetails = _context.BookingDetails.ToList().Where(m => m.UserId == userMaster.FirstOrDefault().UserId);
                IEnumerable<UserBookingDetail> userBookingDetails = _context.UserBookingDetails.ToList().Where(m => m.Pnr == bookingDetails.FirstOrDefault().Pnr);

                var result = (from p in userBookingDetails
                              join t in bookingDetails on p.Pnr equals t.Pnr
                              join c in userMaster on t.UserId equals c.UserId
                              where c.EmailId == emailId
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

                return Ok(new { emailId, result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Response = "Error", ResponseMessage = ex.Message });
            }
        }

        [HttpDelete("cancel/{pnr}")]
        public IActionResult CancelBooking(int pnr)
        {
            try
            {
                var resultBookingDetails = _context.BookingDetails.Where(m => m.Pnr == pnr);
                _context.BookingDetails.Remove((BookingDetail)resultBookingDetails);

                var resultUserBookingDetails = _context.UserBookingDetails.Where(m => m.Pnr == pnr);
                _context.UserBookingDetails.Remove((UserBookingDetail)resultUserBookingDetails);

                if (resultBookingDetails.ToList().Count == 0 || resultUserBookingDetails.ToList().Count == 0)
                {
                    return NotFound("No records found with PNR: " + pnr);
                }

                _context.SaveChanges();

                var message = "Booking for PNR No: " + pnr + " is cancelled successfully";

                return Accepted(message);

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
