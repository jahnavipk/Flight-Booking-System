using CommonDAL.Models;
using CommonDAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommonDAL.Interfaces
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(TblUserMaster users);
    }
}
