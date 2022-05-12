using AirlineService.Interfaces;
using CommonDAL.Models;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AirlineService.Models
{
    public class AirlineRepository : IAirlineRepository, IConsumer<InventoryModificationDetails>
    {
        FlightBookingDBContext _context;

        public AirlineRepository(FlightBookingDBContext context)
        {
            _context = context;
        }

        public int RegisterUser(TblUserMaster userDetails)
        {
            using (SHA512 sha512hash = SHA512.Create())
            {
                byte[] sourceBytes = Encoding.UTF8.GetBytes(userDetails.Password);
                byte[] hashBytes = sha512hash.ComputeHash(sourceBytes);
                string hashedPassword = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

                userDetails.Password = hashedPassword;
            }

            userDetails.IsActive = "Y";
            userDetails.CreatedBy = userDetails.UserName.ToString();
            userDetails.ModifiedBy = userDetails.UserName.ToString();

            _context.TblUserMasters.Add(userDetails);
            int IsSuccess = _context.SaveChanges();

            return IsSuccess;
        }


        public int AddFlightDetails(TblFlightMaster inventoryDetails)
        {
            inventoryDetails.IsActive = "Y";
            inventoryDetails.CreatedBy = inventoryDetails.ModifiedBy = "Admin";

            _context.TblFlightMasters.Add(inventoryDetails);
            int IsSuccess = _context.SaveChanges();

            return IsSuccess;
        }

        public Task Consume(ConsumeContext<InventoryModificationDetails> context)
        {
            string FlightNo = context.Message.FlightNo;
            int NoOfSeats = context.Message.NoOfSeats;
            DateTime DepartureDateTime = context.Message.DepartureDateTime;

            TblFlightMaster flightDetails = _context.TblFlightMasters
                .FirstOrDefault(m => m.FlightNo == context.Message.FlightNo
                && m.DepartureDateTime == context.Message.DepartureDateTime 
                && m.IsActive == "Y");

            if (context.Message.Action == "Book")
            {
                //_context.Attach(flightDetails);
                flightDetails.NoOfSeats = flightDetails.NoOfSeats - NoOfSeats;
                //_context.TblFlightMasters.Add(flightDetails);
                _context.SaveChanges();
            }
            else if (context.Message.Action == "Cancel")
            {
                flightDetails.NoOfSeats = flightDetails.NoOfSeats + NoOfSeats;
                _context.TblFlightMasters.Add(flightDetails);
                _context.SaveChanges();
            }

            return Task.CompletedTask;
        }
    }
}
