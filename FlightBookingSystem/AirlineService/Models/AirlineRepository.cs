using AirlineService.Interfaces;
using CommonDAL.Models;
using CommonDAL.Repositories;
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


        public int AddFlightDetails(FlightMaster[] inventoryDetails)
        {
            int IsSuccess = 0;
            foreach (var item in inventoryDetails)
            {
                TblFlightMaster tblFlightMaster = new TblFlightMaster();


                tblFlightMaster.FlightNo = item.FlightNo;
                tblFlightMaster.FlightName = item.FlightName;
                tblFlightMaster.FromLocation = item.FromLocation;
                tblFlightMaster.ToLocation = item.ToLocation;
                tblFlightMaster.NoOfSeats = item.NoOfSeats;
                tblFlightMaster.Price = item.Price;
                tblFlightMaster.MealOption = item.MealOption;
                tblFlightMaster.Remarks = item.Remarks;
                tblFlightMaster.DepartureDateTime = Convert.ToDateTime(item.DepartureDate + "T" + item.DepartureTime);
                tblFlightMaster.ArrivalDateTime = Convert.ToDateTime(item.ArrivalDate + "T" + item.ArrivalTime);

                tblFlightMaster.IsActive = item.IsActive = "Y";
                tblFlightMaster.CreatedBy = tblFlightMaster.ModifiedBy = item.CreatedBy = item.ModifiedBy = "Admin";

                _context.TblFlightMasters.Add(tblFlightMaster);
                IsSuccess = _context.SaveChanges();
            }
            //inventoryDetails.IsActive = "Y";
            //inventoryDetails.CreatedBy = inventoryDetails.ModifiedBy = "Admin";

            //_context.TblFlightMasters.Add(inventoryDetails);
            //int IsSuccess = _context.SaveChanges();

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
                flightDetails.NoOfSeats = flightDetails.NoOfSeats - NoOfSeats;
                _context.SaveChanges();
            }
            else if (context.Message.Action == "Cancel")
            {
                flightDetails.NoOfSeats = flightDetails.NoOfSeats + NoOfSeats;
                _context.SaveChanges();
            }

            return Task.CompletedTask;
        }
    }
}
