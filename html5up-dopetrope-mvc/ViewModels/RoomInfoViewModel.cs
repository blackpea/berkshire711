using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace html5up_dopetrope_mvc.ViewModels
{
    using html5up_dopetrope_mvc.Models;

    public class RoomInfoViewModel
    {
        public TB_Room Room { get; set; }

        public TB_RoomCharge RoomCharge { get; set; }
    }
}
