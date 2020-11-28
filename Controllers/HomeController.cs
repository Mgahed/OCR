using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tesseract;

namespace MyOCR.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Result = false;
            return View();
        }

        public ActionResult submit(HttpPostedFileBase file)
        {
            var lang = Request.Form["lang"];
            if (file == null || file.ContentLength == 0)
            {
                ViewBag.Result = true;
                ViewBag.res = "File not Found";
                return View("Index");
            }

            if (lang == "ara")
            {
                using (var engine = new TesseractEngine(Server.MapPath(@"~/TesseractData"), "ara", EngineMode.Default))
                {
                    using (var image = new System.Drawing.Bitmap(file.InputStream))
                    {
                        using (var pix = PixConverter.ToPix(image))
                        {
                            using (var page = engine.Process(pix))
                            {
                                ViewBag.Result = true;
                                ViewBag.res = page.GetText();
                                ViewBag.mean = String.Format("{0:p}", page.GetMeanConfidence());
                                return View("Index");
                            }
                        }
                    }
                }
            }
            using (var engine = new TesseractEngine(Server.MapPath(@"~/TesseractData"), "eng", EngineMode.Default))
            {
                using (var image = new System.Drawing.Bitmap(file.InputStream))
                {
                    using (var pix = PixConverter.ToPix(image))
                    {
                        using (var page = engine.Process(pix))
                        {
                            ViewBag.Result = true;
                            ViewBag.res = page.GetText();
                            ViewBag.mean = String.Format("{0:p}", page.GetMeanConfidence());
                            return View("Index");
                        }
                    }
                }
            }
        }
        // public ActionResult Index()
        // {
        //     return View();
        // }
        //
        // public ActionResult About()
        // {
        //     ViewBag.Message = "Your application description page.";
        //
        //     return View();
        // }
        //
        // public ActionResult Contact()
        // {
        //     ViewBag.Message = "Your contact page.";
        //
        //     return View();
        // }
    }
}