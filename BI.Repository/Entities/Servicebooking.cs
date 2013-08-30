using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BI.Repository.Entities
{
    public class ServiceBooking
    {
        public int Id { get; set; }
        public DateTime DateStamp { get; set; }
        public string Booking { get; set; }
        public DateTime DateWanted { get; set; }
        public string RegNr { get; set; }
        public string RegDate { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public string FirmName { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string EmailCustomer { get; set; }
        public string EmailDealer { get; set; }
        public string Miles { get; set; }

    }
}
