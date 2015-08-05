using html5up_dopetrope_mvc.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace html5up_dopetrope_mvc.Areas.Admin.Controllers
{

    using PagedList;
    using System.IO;

    public class TB_RoomController : Controller
    {
        private BedAndBreakfastEntities db = new BedAndBreakfastEntities();

        private int pageSize = 10;
        
        // GET: Admin/TB_Room
        public ActionResult Index(int page = 1)
        {
            int currentPage = page < 1 ? 1 : page;

            return View(db.TB_Room.OrderByDescending(desc => desc.ID).ToPagedList(currentPage, pageSize));
        }

        // GET: Admin/TB_Room/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_Room tB_Room = db.TB_Room.Find(id);
            if (tB_Room == null)
            {
                return HttpNotFound();
            }
            return View(tB_Room);
        }

        // GET: Admin/TB_Room/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/TB_Room/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TB_Room tB_Room
            , IEnumerable<HttpPostedFileBase> files)
        {
            tB_Room.ImgUrl1 = "";
            tB_Room.ImgUrl2 = "";
            tB_Room.ImgUrl3 = "";
            tB_Room.ImgUrl4 = "";
            tB_Room.ImgUrl5 = "";

            if (ModelState.IsValid)
            {
                db.TB_Room.Add(tB_Room);
                db.SaveChanges();

                this.UploadFiles(tB_Room, files);

                return RedirectToAction("Index");
            }

            return View(tB_Room);
        }

        private void UploadFiles(TB_Room tB_Room, IEnumerable<HttpPostedFileBase> files)
        {
            try
            {
                if (files != null && files.Any())
                {
                    foreach (var uploadfile in files)
                    {
                        if (uploadfile != null && uploadfile.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(uploadfile.FileName);
                            var path = Path.Combine(this.Server.MapPath("~/UploadFiles/TB_Room/" + tB_Room.ID), fileName);

                            var ext  = Path.GetExtension(uploadfile.FileName);

                            //if (!ext.Equals(".jpg") || !ext.Equals(".png") || !ext.Equals(".gif") || !ext.Equals(".jpeg"))
                            //{
                            //    continue;
                            //}

                            if (!new String[] { ".jpg", ".png", ".gif", ".jpeg" }.Contains(ext))
                            {
                                continue;
                            }

                            // 檔案目錄不存在時，建立目錄
                            if (!Directory.Exists(this.Server.MapPath("~/Upload/TB_Room/" + tB_Room.ID)))
                            {
                                Directory.CreateDirectory(this.Server.MapPath("~/UploadFiles/TB_Room/" + tB_Room.ID));
                            }

                            // 檔案存在時，做覆蓋檔案
                            if (System.IO.File.Exists(path))
                            {
                                System.IO.File.Delete(path);
                            }

                            uploadfile.SaveAs(path);
                        }
                    }
                }
            }
            catch
            {
                this.ViewBag.Message = "Upload failed";
            }
        }

        // GET: Admin/TB_Room/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_Room tB_Room = db.TB_Room.Find(id);
            if (tB_Room == null)
            {
                return HttpNotFound();
            }
            return View(tB_Room);
        }

        // POST: Admin/TB_Room/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TB_Room tB_Room
            , IEnumerable<HttpPostedFileBase> files)
        {
            tB_Room.ImgUrl1 = "";
            tB_Room.ImgUrl2 = "";
            tB_Room.ImgUrl3 = "";
            tB_Room.ImgUrl4 = "";
            tB_Room.ImgUrl5 = "";

            if (ModelState.IsValid)
            {
                db.Entry(tB_Room).State = EntityState.Modified;
                db.SaveChanges();

                this.UploadFiles(tB_Room, files);

                return RedirectToAction("Index");
            }
            return View(tB_Room);
        }

        // GET: Admin/TB_Room/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_Room tB_Room = db.TB_Room.Find(id);
            if (tB_Room == null)
            {
                return HttpNotFound();
            }

            return View(tB_Room);
        }

        public ActionResult DeleteUploadFile(int id, string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TB_Room tB_Room = db.TB_Room.Find(id);
            if (tB_Room == null)
            {
                return HttpNotFound();
            }

            try
            {
                imagePath = imagePath.TrimStart("~/".ToCharArray());
                
                System.IO.File.Delete(Path.GetFullPath(System.IO.Path.GetFullPath(System.IO.Path.Combine(this.Server.MapPath("~"), imagePath))));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Edit", "TB_Room", new { id = id });
            }

            return RedirectToAction("Edit", "TB_Room", new { id = id });
        }


        // POST: Admin/TB_Room/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TB_Room tB_Room = db.TB_Room.Find(id);
            db.TB_Room.Remove(tB_Room);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
