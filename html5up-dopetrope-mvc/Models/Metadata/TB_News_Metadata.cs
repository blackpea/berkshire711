using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace html5up_dopetrope_mvc.Models
{
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(TB_News.TB_NewsMetadata))]
    public partial class TB_News
    {
        public class TB_NewsMetadata
        {
            [Key]
            public int ID { get; set; }
            [Required]
            [Display(Name = "發佈日期")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public System.DateTime PostDateTime { get; set; }
            [Required]
            [Display(Name = "標題")]
            public string Title { get; set; }
            [Required]
            [Display(Name = "描述說明")]
            [DataType(DataType.MultilineText)]
            public string Description { get; set; }
            
            [Display(Name = "是否刪除")]
            public bool IsDelete { get; set; }

        }
    }
}