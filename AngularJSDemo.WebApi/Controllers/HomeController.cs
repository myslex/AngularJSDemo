using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AngularJSDemo.MvcWebApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() { return View(); }

        public ActionResult Movies() { return View(); }

        public ActionResult Directors() { return View(); }
    }
}
