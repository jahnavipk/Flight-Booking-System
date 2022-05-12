using System;
using System.Collections.Generic;

#nullable disable

namespace CommonDAL.Models
{
    public partial class TblStatusMaster
    {
        public int StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public string IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
