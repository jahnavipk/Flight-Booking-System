using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineService.Models
{
    public class UserDetails
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime? DoB { get; set; }
        public string Gender { get; set; }
        public string EmailId { get; set; }
        public string ContactNo { get; set; }
        public char IsActive { get; set; }
    }
}
