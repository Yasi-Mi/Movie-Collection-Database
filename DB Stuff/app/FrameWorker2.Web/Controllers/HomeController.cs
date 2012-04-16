using System.Collections.Generic;
using System.Web.Mvc;
using SharpLite.Domain.DataInterfaces;
using MovieDatabase.Domain;
using System.Collections;

namespace MovieDatabase.Web.Controllers
{
    public class HomeController : Controller
    {
      
        public ActionResult Index()
        {
            return View();
        }

        
    }
}
