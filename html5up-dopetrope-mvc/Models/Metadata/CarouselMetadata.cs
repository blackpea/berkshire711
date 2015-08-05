namespace html5up_dopetrope_mvc.Models
{
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(TB_Carousel.TB_CarouselMetadata))]
    public partial class TB_Carousel
    {
        public class TB_CarouselMetadata
        {
            [Key]
            public int ID { get; set; }

           
            [Display(Name = "圖片")]
            public byte[] ImageData { get; set; }
           
            [Display(Name = "發佈日期")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
 
            public System.DateTime DateUploaded { get; set; }

           
            [Display(Name = "描述說明")]
            public string Description { get; set; }
            public bool IsActive { get; set; }
            public string PhotoMimeType { get; set; }
            public string PhotoName { get; set; }

        }
    }
}
