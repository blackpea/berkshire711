using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace html5up_dopetrope_mvc.Areas.Admin.Controllers
{
    using System.IO;
    using System.Net;

    using html5up_dopetrope_mvc.Models;

    using PagedList;

    public class CarouselController : Controller
    {
        private BedAndBreakfastEntities db = new BedAndBreakfastEntities();

        private int pageSize = 10;

        // GET: Admin/Carousel
        public ActionResult Index(int page = 1)
        {
            int currentPage = page < 1 ? 1 : page;

            var TB_CarouselImages = db.TB_Carousel.OrderByDescending(desc => desc.DateUploaded);

            return View(TB_CarouselImages.ToPagedList(currentPage, pageSize));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TB_Carousel CarouselImage, HttpPostedFileBase UploadImages)
        {
            if (ModelState.IsValid)
            {
                CarouselImage.DateUploaded = DateTime.Now;
                CarouselImage.IsActive = true;
                CarouselImage.Description = CarouselImage.Description ?? "";

                using (var memoryStream = new MemoryStream())
                {
                    UploadImages.InputStream.CopyTo(memoryStream);
                    CarouselImage.ImageData = memoryStream.ToArray();
                }
 
                CarouselImage.PhotoMimeType = UploadImages.ContentType;
                CarouselImage.PhotoName = UploadImages.FileName;

                db.TB_Carousel.Add(CarouselImage);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            //we only get here if there was a problem
            return View(CarouselImage);
        }

        [HttpGet]
        public ActionResult GetImage(int id)
        {
            var agent = db.TB_Carousel.FirstOrDefault(w => w.ID == id);
            if (agent != null)
            {
                return File(agent.ImageData, agent.PhotoMimeType, agent.PhotoName);
            }
            return Content("");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_Carousel tB_News = db.TB_Carousel.Find(id);
            if (tB_News == null)
            {
                return HttpNotFound();
            }
            return View(tB_News);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TB_Carousel tB_News = db.TB_Carousel.Find(id);
            db.TB_Carousel.Remove(tB_News);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}