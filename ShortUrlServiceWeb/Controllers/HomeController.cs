using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ShortUrlServiceWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult History()
        {
            ViewBag.Title = "History";

            return View();
        }

        public RedirectResult Record()
        {
            var length = Request.Url.Segments.Length;
            string hash = Request.Url.Segments[length - 1];
            UriBuilder uriBuilder = new System.UriBuilder();
            uriBuilder.Scheme = Request.Url.Scheme;
            uriBuilder.Host = Request.Url.Host;
            uriBuilder.Port = Request.Url.Port;
            uriBuilder.Path = $"/api/records/{hash}";
            var apiUri = uriBuilder.Uri;

            return RedirectPermanent(apiUri.AbsoluteUri);
        }


    }
}
