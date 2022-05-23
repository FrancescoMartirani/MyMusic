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

        [HttpGet]
        public IActionResult Index()
        {
            return View(dbManager.getBrani());
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Brano brano)
        {
            dbManager.aggiungiBrano(brano);
            return RedirectToAction("Index");
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