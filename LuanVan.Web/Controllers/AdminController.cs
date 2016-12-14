using LuanVan.Web.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LuanVan.Web.Controllers
{
    [LVAuthorize(RoleType.Admin)]
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}
