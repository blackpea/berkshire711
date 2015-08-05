using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace html5up_dopetrope_mvc.Models
{
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(TB_Room.TB_RoomMetadata))]
    public partial class TB_Room
    {
        public class TB_RoomMetadata
        {

            [Key]
            public int ID { get; set; }

            [Required]
            [Display(Name = "房型名稱")]
            public string Name { get; set; }

            [Required]
            [Display(Name = "房型坪數")]
            public double SquareFootage { get; set; }

            [Required]
            [Display(Name = "最大允許人數")]
            public int MaxCapacity { get; set; }

            [Required]
            [Display(Name = "房型設施")]
            [DataType(DataType.MultilineText)]
            public string Facility { get; set; }

            [Required]
            [Display(Name = "房型特色")]
            [DataType(DataType.MultilineText)]
            public string Features { get; set; }

            [Display(Name = "是否刪除")]
            //[DataType(DataType.)]
            public bool IsDelete { get; set; }

            //[Required]
            [Display(Name = "圖片一")]
            //[DataType(DataType.Upload)]
            public string ImgUrl1 { get; set; }

            //[Required]
            [Display(Name = "圖片二")]
            //[DataType(DataType.Upload)]
            public string ImgUrl2 { get; set; }

            //[Required]
            [Display(Name = "圖片三")]
            //[DataType(DataType.ImageUrl)]
            public string ImgUrl3 { get; set; }

            //[Required]
            [Display(Name = "圖片四")]
            //[DataType(DataType.Upload)]
            public string ImgUrl4 { get; set; }

            //[Required]
            [Display(Name = "圖片五")]
            //[DataType(DataType.Upload)]
            public string ImgUrl5 { get; set; }

            [Required]
            [Display(Name = "可加床人數")]
            public int ExtraBed { get; set; }

            [Required]
            [Display(Name = "房型日曆顏色")]
            public string Color { get; set; }

            [Display(Name = "訂房單據")]
            public virtual ICollection<TB_RoomBookingOrder> TB_RoomBookingOrder { get; set; }

            [Display(Name = "房型金額")]
            public virtual ICollection<TB_RoomCharge> TB_RoomCharge { get; set; }

        }
    }
}