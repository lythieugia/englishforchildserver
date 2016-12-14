using LuanVan.Web.Models.Domain;
using LuanVan.Web.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LuanVan.Web.Controllers
{
    [LVAuthorize]
    public class HomeController : Controller
    {
        private LuanVanDbContext db = new LuanVanDbContext();

        public ActionResult Index()
        {
            string roleName = db.UserProfiles.Where(u => u.UserName == User.Identity.Name).Select(u => u.RoleName).FirstOrDefault();
            RoleType rType = (RoleType)Enum.Parse(typeof(RoleType), roleName);

            if (rType == RoleType.Parent)
            {
                return RedirectToAction("Student", "Report");
            }

            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
