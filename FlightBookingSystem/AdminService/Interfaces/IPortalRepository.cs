using CommonDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminService.Interfaces
{
    public interface IPortalRepository
    {
        public bool Login(TblUserMaster userLogin);
    }
}
