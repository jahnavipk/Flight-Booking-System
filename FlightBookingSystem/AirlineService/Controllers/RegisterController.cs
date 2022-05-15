using AirlineService.Interfaces;
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

/// <summary>
/// Author: Jahnavi Kamatgi
/// Purpose: Register a new user
/// </summary>
namespace AirlineService.Controllers
{
    [Route("api/airline/register")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        IAirlineRepository _context;

        public RegisterController(IAirlineRepository context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult RegisterUser(TblUserMaster userDetails)
        {
            try
            {
                int IsRegisteredSuccessfully = _context.RegisterUser(userDetails);

                if (IsRegisteredSuccessfully > 0)
                {
                    return Ok(new { response = "User Registered successfully" });
                }
                else
                {
                    return BadRequest(new { response = "User could not be registered" });
                }
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
