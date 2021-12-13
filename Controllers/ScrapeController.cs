using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using gamed.Models;
using System.Net.Http;

namespace gamed.Controllers
{
    public class ScrapeController : Controller
    {
        private readonly ILogger<ScrapeController> _logger;
		private  IIGDBService _iGDBService;

        public ScrapeController(ILogger<ScrapeController> logger,IIGDBService iGDBService)
        {
            _logger = logger;
			_iGDBService = iGDBService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Platforms()
        {
			var platforms = Task.Run(() => _iGDBService.GetPlatforms());
			platforms.Wait();
			return View(platforms.Result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
