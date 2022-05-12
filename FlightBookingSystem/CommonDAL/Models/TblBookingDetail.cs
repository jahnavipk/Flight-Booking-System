using System;
using System.Collections.Generic;

#nullable disable

namespace CommonDAL.Models
{
    public partial class TblBookingDetail
    {
        public int Pnr { get; set; }
        public int UserId { get; set; }
        public string FlightNo { get; set; }
        public int NoOfPassengers { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public string IsOneWay { get; set; }
        public DateTime? ReturnDateTime { get; set; }
        public int StatusCode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
