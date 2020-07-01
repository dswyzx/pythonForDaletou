using DotNetCoreTest.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DotNetCoreTest.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //GetAllInfo.CrawlLatestDLTInfo(2);
            List<DLTInfo> lstDlt = GetAllInfo.GetNewList(10);
            return View(lstDlt);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
