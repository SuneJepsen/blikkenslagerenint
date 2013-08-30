using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BI.Repository.Entities
{
    public class Contact
    {
        public int Id { get; set; }
        public string bodyText { get; set; }
        public string Subject { get; set; }
        public string EmailTo { get; set; }
        public string EmailFrom { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
