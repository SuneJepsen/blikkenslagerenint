using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BI.Umbraco.helpers
{
    public class EmailStatus
    {
        public bool EmailSent { get; set; }
        public bool ConfirmEmailSent { get; set; }
        public string Exception { get; set; }
        public string StatusMessage { get; set; }
    }
}