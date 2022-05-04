using AuthenticationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Interfaces
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(Users user);
    }
}
