using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NLog;
using DotNetCoreTest.Models;

namespace DotNetCoreTest.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            GetAllInfo.CrawlLatestDLTInfo(2);
            List<DLTInfo> lstDlt = GetAllInfo.GetNewList(10);
            return View(lstDlt);
            //  return View();
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
