using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop2.WebUI.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminNavController : Controller
    {
        // GET: AdminNav
        public PartialViewResult Index()
        {
            return PartialView();
        }
    }
}