using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using html5up_dopetrope_mvc.Models;

namespace html5up_dopetrope_mvc.Areas.Admin.Controllers
{
    using PagedList;

    public class TB_NewsController : Controller
    {
        private BedAndBreakfastEntities db = new BedAndBreakfastEntities();

        private int pageSize = 10;

        // GET: Admin/TB_News
        public ActionResult Index(int page = 1)
        {
            int currentPage = page < 1 ? 1 : page;

            return View(db.TB_News.OrderByDescending(desc => desc.PostDateTime).ToPagedList(currentPage, pageSize));
        }

        // GET: Admin/TB_News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_News tB_News = db.TB_News.Find(id);
            if (tB_News == null)
            {
                return HttpNotFound();
            }
            return View(tB_News);
        }

        // GET: Admin/TB_News/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/TB_News/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PostDateTime,Title,Description,IsDelete")] TB_News tB_News)
        {
            if (ModelState.IsValid)
            {
                db.TB_News.Add(tB_News);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tB_News);
        }

        // GET: Admin/TB_News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_News tB_News = db.TB_News.Find(id);
            if (tB_News == null)
            {
                return HttpNotFound();
            }
            return View(tB_News);
        }

        // POST: Admin/TB_News/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PostDateTime,Title,Description,IsDelete")] TB_News tB_News)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tB_News).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tB_News);
        }

        // GET: Admin/TB_News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_News tB_News = db.TB_News.Find(id);
            if (tB_News == null)
            {
                return HttpNotFound();
            }
            return View(tB_News);
        }

        // POST: Admin/TB_News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TB_News tB_News = db.TB_News.Find(id);
            db.TB_News.Remove(tB_News);
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
