using System;
using System.Collections.Generic;

#nullable disable

namespace CommonDAL.Models
{
    public partial class UserBookingDetail
    {
        public int Pnr { get; set; }
        public string PassengerName { get; set; }
        public int PassengerAge { get; set; }
        public string PassengerGender { get; set; }
        public string IsMealOpted { get; set; }
        public string MealType { get; set; }
        public decimal? Price { get; set; }
        public int StatusCode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int PassengerId { get; set; }
    }
}
