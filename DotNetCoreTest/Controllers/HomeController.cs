using AutoGetDLT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace AutoGetDLT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //GetAllInfo.CrawlLatestDLTInfo(2);
            List<DLTInfo> lstDlt = GetAllInfo.GetNewList(10);
            return View(lstDlt);
        }

        public IActionResult About()
        {
            // _logger.LogTrace(1, "LogTrace NLog injected into HomeController");
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
