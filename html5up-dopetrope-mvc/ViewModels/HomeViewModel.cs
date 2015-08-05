using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace html5up_dopetrope_mvc.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    using html5up_dopetrope_mvc.Models;

    public class HomeViewModel
    {
        /// <summary>
        /// 最新消息清單
        /// </summary>
        public IQueryable<TB_News> TbNewsList;

        /// <summary>
        /// 我們的客房清單
        /// </summary>
        public IQueryable<TB_Room> TbRoomsList;

        public TB_RoomBookingOrder RoomBookingOrder { get; set; }

        [Required]
        [Display(Name = "入住日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime OrderDateForm { get; set; }

        [Required]
        [Display(Name = "退房日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime OrderDateTo { get; set; }

        [Required]
        [Range(0, 6)]
        [Display(Name = "房間數")]
        public int RoomNum { get; set; }

        [Required]
        [Range(0, 25)]
        [Display(Name = "大人人數")]
        public int AdultNum { get; set; }

        [Required]
        [Range(0, 25)]
        [Display(Name = "小孩人數")]
        public int ChildNum { get; set; }
    }
}