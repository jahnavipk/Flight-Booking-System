using CommonDAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdminService.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        FlightBookingDBContext _context;

        public LoginController(FlightBookingDBContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login(UserMaster userLogin)
        {
            try
            {
                using (SHA512 sha512hash = SHA512.Create())
                {
                    byte[] sourceBytes = Encoding.UTF8.GetBytes(userLogin.Password);
                    byte[] hashBytes = sha512hash.ComputeHash(sourceBytes);
                    string hashedPassword = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

                    userLogin.Password = hashedPassword;
                }

                IEnumerable<UserMaster> searchResults = _context.UserMasters.ToList()
                    .Where(m => m.EmailId == userLogin.EmailId && m.Password == userLogin.Password);

                //Check if the entered credentials are found in the DB
                if (searchResults.ToList().Count != 0)
                {
                    return Ok();
                }

                return Unauthorized("Incorrect Email Id/ Password");
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
