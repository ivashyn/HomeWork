using MVCClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace MVCClient.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        [Authorize]
        public ActionResult Identity()
        {
            return View(GetClaimsViewModel());
        }

        private ClaimsViewModel GetClaimsViewModel()
        {
            var identity = (ClaimsIdentity)HttpContext.User.Identity;

            return new ClaimsViewModel
            {
                Header = "Claims for Authorized User",
                Message = "The following claims have been retrieved from the Identity Server",
                Claims = identity.Claims.Select(x => new KeyValuePair<string, string>(x.Type, x.Value)),
                IsAuthenticated = HttpContext.User.Identity.IsAuthenticated,
            };
        }




        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}