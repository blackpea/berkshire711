using html5up_dopetrope_mvc.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace html5up_dopetrope_mvc.Controllers
{
    using html5up_dopetrope_mvc.Models;

    public class NewsController : Controller
    {
        private readonly NewsRepository newsRepository;

        public NewsController()
        {
            newsRepository = new NewsRepository();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                newsRepository.Dispose();
 
            }
            base.Dispose(disposing);
        }

        // GET: News
        public ActionResult Index()
        {
            var  viewModel = newsRepository.Find<TB_News>(find => find.IsDelete == false).Take(10).OrderByDescending( desc => desc.PostDateTime);
            return View(viewModel);
        }
    }
}