using AdminService.Interfaces;
using CommonDAL.Interfaces;
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
        private readonly IJWTManagerRepository _jWTManager;
        public LoginController(IPortalRepository context, IJWTManagerRepository jWTManager)
        {
            _context = context;
            _jWTManager = jWTManager;
        }

        [HttpPost("login")]
        public IActionResult Login(TblUserMaster userLogin)
        {
            try
            {
                List<string> result = _context.Login(userLogin);

                if (result.Count == 0)
                {
                    return Unauthorized("Incorrect Email Id/ Password");
                }

                var token = _jWTManager.Authenticate(userLogin);

                if (token == null)
                {
                    return Unauthorized();
                }

                Dictionary<string, string> lst = new Dictionary<string, string>();

                lst.Add("userId", result[0].ToString());
                lst.Add("roleId", result[1].ToString());
                lst.Add("userEmailId", result[2].ToString());
                lst.Add("userName", result[3].ToString());
                lst.Add("userContactNO", result[4].ToString());
                lst.Add("token", token.Token);
                lst.Add("refreshToken", token.RefreshToken);

                return Ok(lst);
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
