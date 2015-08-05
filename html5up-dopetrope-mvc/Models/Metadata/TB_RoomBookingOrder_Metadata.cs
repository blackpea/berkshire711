using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace html5up_dopetrope_mvc.Models
{
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(TB_RoomBookingOrder.TB_RoomBookingOrderMetadata))]
    public partial class TB_RoomBookingOrder
    {
        public class TB_RoomBookingOrderMetadata
        {
            [Key]
            public int ID { get; set; }

            [Required(ErrorMessage = "必填")]
            [Display(Name = "是否加床")]
            public bool IsExtraBed { get; set; }

            [Required(ErrorMessage = "必填")]
            [Display(Name = "加床人數")]
            [Range(0, 2)]
            public int ExtraBadNum { get; set; }

            [Required(ErrorMessage = "必填")]
            [Display(Name = "住房日期(起)")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public System.DateTime OrderDateForm { get; set; }

            [Required(ErrorMessage = "必填")]
            [Display(Name = "住房日期(訖)")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public System.DateTime OrderDateTo { get; set; }

            [Display(Name = "是否刪除")]
            public bool IsDelete { get; set; }

            [Required]
            [Display(Name = "建立日期")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public System.DateTime CreateDateTime { get; set; }

            [Required]
            [Display(Name = "房間數")]
            public int RoomNum { get; set; }

            [Required(ErrorMessage = "必填")]
            [Range(1, 4)]
            [Display(Name = "大人人數")]
            public int AdultNum { get; set; }

            [Required(ErrorMessage = "必填")]
            [Range(0, 4)]
            [Display(Name = "小孩人數")]
            public int ChildNum { get; set; }
           
            [Required(AllowEmptyStrings = false)]
            [Display(Name = "顧客名稱")]
            public string CustomeName { get; set; }
            
            [Required(AllowEmptyStrings = false)]
            [Display(Name = "顧客Email")]
            [DataType(DataType.EmailAddress)]
            public string CustomeEmail { get; set; }
          
            [Display(Name = "顧客性別")]
            public string CustomeSex { get; set; }

            [Required(AllowEmptyStrings = false)]
            [Display(Name = "顧客連絡電話")]
            public string CustomeTel { get; set; }
          
            [Display(Name = "總金額")]
            [DataType(DataType.Currency)]
            public int TotalAmount { get; set; }
          
            [Display(Name = "訂單單號")]
            public string OrderNo { get; set; }
          
            [Display(Name = "是否已完成")]
            public bool IsComplete { get; set; }
           
            [Display(Name = "房型Id")]
            public Nullable<int> RoomId { get; set; }
           
            [Display(Name = "房型資訊")]
            public virtual TB_Room TB_Room { get; set; }

        }
    }
}