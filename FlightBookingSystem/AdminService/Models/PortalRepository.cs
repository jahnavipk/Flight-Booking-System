using AdminService.Interfaces;
using CommonDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdminService.Models
{
    public class PortalRepository : IPortalRepository
    {
        FlightBookingDBContext _context;

        public PortalRepository(FlightBookingDBContext context)
        {
            _context = context;
        }

        public List<string> Login(TblUserMaster userLogin)
        {
            using (SHA512 sha512hash = SHA512.Create())
            {
                byte[] sourceBytes = Encoding.UTF8.GetBytes(userLogin.Password);
                byte[] hashBytes = sha512hash.ComputeHash(sourceBytes);
                string hashedPassword = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

                userLogin.Password = hashedPassword;
            }

            IEnumerable<TblUserMaster> searchResults = _context.TblUserMasters.ToList()
                .Where(m => m.EmailId == userLogin.EmailId && m.Password == userLogin.Password);

            List<string> lst = new List<string>();
            //Check if the entered credentials are found in the DB
            if (searchResults.ToList().Count != 0)
            {
                lst.Add(searchResults.FirstOrDefault().UserId.ToString());
                lst.Add(searchResults.FirstOrDefault().RoleId.ToString());
                lst.Add(searchResults.FirstOrDefault().EmailId.ToString());
                lst.Add(searchResults.FirstOrDefault().UserName.ToString());
                lst.Add(searchResults.FirstOrDefault().ContactNo.ToString());
            }

            return lst;
        }
    }
}
