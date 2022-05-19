using BookingService.Interfaces;
using BookingService.Models;
using CommonDAL.Models;
using CommonDAL.Repositories;
using MassTransit.KafkaIntegration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Author: Jahnavi Kamatgi
/// Purpose: Manages Flight bookings - Book a flight, get booking history for a user, delete ticket for a user
/// </summary>
namespace BookingService.Controllers
{
    [Route("api/booking")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        IBookFlightsRepository _context;
        FlightBookingDBContext _dbContext;
        private ITopicProducer<InventoryModificationDetails> _topicProducer;

        public BookingController(IBookFlightsRepository context, FlightBookingDBContext dbContext, ITopicProducer<InventoryModificationDetails> topicProducer)
        {
            _context = context;
            _dbContext = dbContext;
            _topicProducer = topicProducer;
        }

        [HttpPost]
        public async Task<IActionResult> BookFlight(BookingInputDetails[] bookingInputDetails)
        {
            try
            {
                //Should write code here to check if the user is logged in

                string PNR = _context.BookFlights(bookingInputDetails);

                await _topicProducer.Produce(new InventoryModificationDetails
                {
                    FlightNo = bookingInputDetails.FirstOrDefault().FlightNo,
                    DepartureDateTime = bookingInputDetails.FirstOrDefault().DepartureDateTime,
                    NoOfSeats = bookingInputDetails.FirstOrDefault().TblPassengerDetails.Length,
                    Action = "Book"
                });

                return Ok(new { Response = "Flight Booked Successfully with PNR No: " + PNR });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Response = "Error", ResponseMessage = ex.Message });
            }
        }

        //Gets all bookings for a User
        [HttpGet("history/{emailId}")]
        public IActionResult GetBookingHistory(string emailId)
        {
            try
            {
                IEnumerable<TblUserMaster> userMaster = _dbContext.TblUserMasters.ToList().Where(m => m.EmailId == emailId);
                IEnumerable<TblBookingDetail> bookingDetails = _dbContext.TblBookingDetails.ToList().Where(m => m.UserId == userMaster.FirstOrDefault().UserId);
                //IEnumerable<TblPassengerDetail> userBookingDetails = _dbContext.TblPassengerDetails.ToList().Where(m => m.Pnr == bookingDetails.FirstOrDefault().Pnr) ;

                IEnumerable<TblPassengerDetail> userBookingDetails = _dbContext.TblPassengerDetails.ToList();


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

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Response = "Error", ResponseMessage = ex.Message });
            }
        }

        //Gets booking details for the entered PNR
        [HttpGet("ticket/{pnr}")]
        public IActionResult GetBookingDetails(int pnr)
        {
            try
            {
                TblBookingDetail details = _dbContext.TblBookingDetails.ToList().Find(m => m.Pnr == pnr);

                if (details != null)
                {
                    IEnumerable<TblBookingDetail> bookingDetails = _dbContext.TblBookingDetails.ToList().Where(m => m.Pnr == pnr);
                    IEnumerable<TblPassengerDetail> userBookingDetails = _dbContext.TblPassengerDetails.ToList().Where(m => m.Pnr == pnr);
                    IEnumerable<TblUserMaster> userMaster = _dbContext.TblUserMasters.ToList().Where(m => m.UserId == bookingDetails.FirstOrDefault().UserId);

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

                return NotFound(new { Response = "No records found with the entered PNR number. Please enter the correct PNR number." });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Response = "Sorry! We are unable to process your request.Please try again after some time.",
                    ResponseMessage = ex.Message
                });
            }
        }

        [HttpDelete("cancel/{pnr}")]
        public async Task<IActionResult> CancelBooking(int pnr)
        {
            try
            {
                TblBookingDetail result = new TblBookingDetail();
                result = _context.CancelBooking(pnr);

                if (result != null)
                {
                    await _topicProducer.Produce(new InventoryModificationDetails
                    {
                        FlightNo = result.FlightNo,
                        DepartureDateTime = result.DepartureDateTime,
                        NoOfSeats = result.NoOfPassengers,
                        Action = "Cancel"
                    });

                    return Ok(new { Response = "Booking for PNR No: " + pnr + " is cancelled successfully" });
                }
                else
                {
                    return NotFound("No records found with PNR: " + pnr);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Response = "Sorry! We are unable to process your request. Please try again after some time.",
                    ResponseMessage = ex.Message
                });
            }
        }
    }
}
