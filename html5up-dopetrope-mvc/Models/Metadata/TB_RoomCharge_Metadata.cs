using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace html5up_dopetrope_mvc.Models
{
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(TB_RoomCharge.TB_RoomChargeMetadata))]
    public partial class TB_RoomCharge
    {
        public class TB_RoomChargeMetadata
        {
            [Key]
            public int ID { get; set; }

            [Required]
            [Display(Name = "建立日期")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public System.DateTime CreateDateTime { get; set; }
            
            [Display(Name = "是否刪除")]
            public bool IsDelete { get; set; }

            [Required]
            [Display(Name = "淡季假日住宿金額")]
            [DataType(DataType.Currency)]
            public int LowSeasonHoliday { get; set; }

            [Required]
            [Display(Name = "淡季平日住宿金額")]
            [DataType(DataType.Currency)]
            public int LowSeasonWeekday { get; set; }

            [Required]
            [Display(Name = "旺季假日住宿金額")]
            [DataType(DataType.Currency)]
            public int HighSeasonHoliday { get; set; }

            [Required]
            [Display(Name = "旺季平日住宿金額")]
            [DataType(DataType.Currency)]
            public int HighSeasonWeekday { get; set; }

            [Display(Name = "房型名稱")]
            public int RoomId { get; set; }

            [Required]
            [Display(Name = "每加一床金額")]
            [DataType(DataType.Currency)]
            public int ExtraBedCharge { get; set; }
 
            
            [Display(Name = "房型資訊")]
            public virtual TB_Room TB_Room { get; set; }
        }
    }
}