using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AutoGetDLT.Controllers
{
    public class PageController : Controller
    {

        private readonly ILogger<PageController> _logger;

        public PageController(ILogger<PageController> logger)
        {
            _logger = logger;
            _logger.LogTrace(1, "LogTrace NLog injected into PageController");
            _logger.LogDebug(1, "LogDebug NLog injected into PageController");
            _logger.LogWarning(1, "LogWarning NLog injected into PageController");
            _logger.LogInformation(1, "LogInformation NLog injected into PageController");
            _logger.LogError(1, "LogError NLog injected into PageController");
        }

        // GET: PageController
        public ActionResult About()
        {

            ViewData["Message"] = "mes from Page_About.";

            return View();
        }
    }
}
