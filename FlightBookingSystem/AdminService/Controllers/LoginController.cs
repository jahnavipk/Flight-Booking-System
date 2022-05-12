using AdminService.Interfaces;
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
/// Purpose: Login into the application
/// </summary>
namespace AdminService.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        IPortalRepository _context;

        public LoginController(IPortalRepository context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login(TblUserMaster userLogin)
        {
            try
            {
                bool IsLoginSuccessful = _context.Login(userLogin);

                if (IsLoginSuccessful)
                {
                    return Ok();
                }
                else
                {
                    return Unauthorized("Incorrect Email Id/ Password");
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
