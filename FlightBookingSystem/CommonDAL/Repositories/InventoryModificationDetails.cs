using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommonDAL.Repositories
{
    public class InventoryModificationDetails
    {

        public string FlightNo { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public int NoOfSeats { get; set; }
        public string Action { get; set; }


    }
}
