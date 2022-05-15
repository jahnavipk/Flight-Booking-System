using BookingService.Interfaces;
using CommonDAL.Models;
using CommonDAL.Repositories;
using MassTransit.KafkaIntegration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.Models
{
    public class BookFlightsRepository : IBookFlightsRepository
    {
        FlightBookingDBContext _context;
        private ITopicProducer<InventoryModificationDetails> _topicProducer;

        public BookFlightsRepository(FlightBookingDBContext context, ITopicProducer<InventoryModificationDetails> topicProducer)
        {
            _context = context;
            _topicProducer = topicProducer;
        }
        public string BookFlights(BookingInputDetails[] bookingInputDetails)
        {
            //Insert data into tblBookingDetails table
            TblBookingDetail bookingDetail = new TblBookingDetail();

            foreach(var itemBookingInputDetails in bookingInputDetails)
            {
                bookingDetail.UserId = itemBookingInputDetails.UserId;
                bookingDetail.FlightNo = itemBookingInputDetails.FlightNo;
                bookingDetail.NoOfPassengers = itemBookingInputDetails.TblPassengerDetails.Length;
                bookingDetail.DepartureDateTime = itemBookingInputDetails.DepartureDateTime;
                bookingDetail.IsOneWay = itemBookingInputDetails.IsOneWay;
                bookingDetail.ReturnDateTime = itemBookingInputDetails.ReturnDateTime;
                bookingDetail.StatusCode = 1;
                bookingDetail.CreatedBy = bookingDetail.ModifiedBy = itemBookingInputDetails.UserId.ToString();

                _context.TblBookingDetails.Add(bookingDetail);
                _context.SaveChanges();

                //Insert data into tblPassengerDetails table (Includes passenger wise details)
                foreach (var item in itemBookingInputDetails.TblPassengerDetails)
                {
                    item.Pnr = bookingDetail.Pnr;
                    item.StatusCode = 1;
                    item.CreatedBy = item.ModifiedBy = itemBookingInputDetails.UserId.ToString();

                    _context.TblPassengerDetails.Add(item);
                    _context.SaveChanges();
                }
            }

            return bookingDetail.Pnr.ToString();
        }

        //public string BookFlights(BookingInputDetails bookingInputDetails)
        //{
        //    //Insert data into tblBookingDetails table
        //    TblBookingDetail bookingDetail = new TblBookingDetail();

        //    bookingDetail.UserId = bookingInputDetails.UserId;
        //    bookingDetail.FlightNo = bookingInputDetails.FlightNo;
        //    bookingDetail.NoOfPassengers = bookingInputDetails.NoOfPassengers;
        //    bookingDetail.DepartureDateTime = bookingInputDetails.DepartureDateTime;
        //    bookingDetail.IsOneWay = bookingInputDetails.IsOneWay;
        //    bookingDetail.ReturnDateTime = bookingInputDetails.ReturnDateTime;
        //    bookingDetail.StatusCode = 1;
        //    bookingDetail.CreatedBy = bookingDetail.ModifiedBy = bookingInputDetails.UserId.ToString();

        //    _context.TblBookingDetails.Add(bookingDetail);
        //    _context.SaveChanges();

        //    //Insert data into tblPassengerDetails table (Includes passenger wise details)
        //    foreach (var item in bookingInputDetails.TblPassengerDetails)
        //    {
        //        item.Pnr = bookingDetail.Pnr;
        //        item.StatusCode = 1;
        //        item.CreatedBy = item.ModifiedBy = bookingInputDetails.UserId.ToString();

        //        _context.TblPassengerDetails.Add(item);
        //        _context.SaveChanges();
        //    }

        //    return bookingDetail.Pnr.ToString();
        //}

        public bool CancelBooking(int pnr)
        {
            var resultBookingDetails = _context.TblBookingDetails.Where(m => m.Pnr == pnr);            
            _context.TblBookingDetails.Remove((TblBookingDetail)resultBookingDetails);

            var resultUserBookingDetails = _context.TblPassengerDetails.Where(m => m.Pnr == pnr);
            _context.TblPassengerDetails.Remove((TblPassengerDetail)resultUserBookingDetails);

            if (resultBookingDetails.ToList().Count == 0 || resultUserBookingDetails.ToList().Count == 0)
            {
                return false;
            }
            else
            {
                _context.SaveChanges();
                return true;
            }
        }
    }
}
