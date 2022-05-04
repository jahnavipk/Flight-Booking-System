using AirlineService.Models;
using CommonDAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AirlineService.Controllers
{
    [Route("api/airline/register")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        FlightBookingDBContext _context;

        public RegisterController(FlightBookingDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult RegisterUser(UserMaster userDetails)
        {
            try
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

                _context.UserMasters.Add(userDetails);
                _context.SaveChanges();

                return Created("", "User registered successfully!");
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
