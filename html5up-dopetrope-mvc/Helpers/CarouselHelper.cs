using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace html5up_dopetrope_mvc.Helpers
{
    using System.Text;
    using System.Web.Mvc;

    using html5up_dopetrope_mvc.Models;

    public static class CarouselHelper
    {


        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="target"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static List<TB_Carousel> GetCarouselImages(this HtmlHelper helper )
        {
   
            using (var db =new BedAndBreakfastEntities())
            {
                var images = db.TB_Carousel.OrderByDescending(desc => desc.DateUploaded).ToList();

                return images;
              
               
            }
            
        }


    }

}