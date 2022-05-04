using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchService.Models
{
    public class SearchDetails
    {
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public string DepartureDate { get; set; }
        public string ReturnDate { get; set; }
        public int NoOfPassengers { get; set; }

    }
}
