using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace html5up_dopetrope_mvc.Models
{
    public class CustomDateRange
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; } // If null then it lasts forever
    }
}