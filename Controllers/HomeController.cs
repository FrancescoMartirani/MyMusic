using MyMusic.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using MyMusic.Repository;
using System.Data.SqlClient;

namespace MyMusic.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private DBManager dbManager;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            dbManager = new DBManager();
        }

        public IActionResult Index()
        {
            return View(dbManager.getBrani());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}