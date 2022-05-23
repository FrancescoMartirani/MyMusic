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
        private DBManager_Brani dbManager_Brani;
        private DBManager_Artisti dbManager_Artisti;
        private DBManager_Album dbManager_Album;
        private DBManager_Bands dbManager_Bands;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            dbManager_Brani = new DBManager_Brani();
            dbManager_Artisti = new DBManager_Artisti();
            dbManager_Album = new DBManager_Album();
            dbManager_Bands = new DBManager_Bands();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(dbManager_Brani.getBrani());
        }

        [HttpGet]
        public IActionResult AggiungiBrano()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AggiungiBrano(Brano brano)
        {
            dbManager_Brani.aggiungiBrano(brano);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EliminaBrano(int id)
        {
            var brano = dbManager_Brani.getBrani().Where(x => x.ID == id).FirstOrDefault();
            var delete = dbManager_Brani.eliminaBrano(brano);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Artisti()
        {
            return View(dbManager_Artisti.getArtisti());
        }

        [HttpGet]
        public IActionResult AggiungiArtista()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AggiungiArtista(Artista artista)
        {
            dbManager_Artisti.aggiungiArtista(artista);
            return RedirectToAction("Artisti");
        }

        [HttpGet]
        public IActionResult EliminaArtista(int id)
        {
            var artista = dbManager_Artisti.getArtisti().Where(x => x.ID == id).FirstOrDefault();
            var delete = dbManager_Artisti.eliminaArtista(artista);
            return RedirectToAction("Artisti");

        }

        [HttpGet]
        public IActionResult Album()
        {
            return View(dbManager_Album.getAlbum());
        }

        [HttpGet]
        public IActionResult AggiungiAlbum()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AggiungiAlbum(Album album)
        {
            dbManager_Album.aggiungiAlbum(album);
            return RedirectToAction("Album");
        }

        [HttpGet]
        public IActionResult EliminaAlbum(int id)
        {
            var album = dbManager_Album.getAlbum().Where(x => x.ID == id).FirstOrDefault();
            var delete = dbManager_Album.eliminaAlbum(album);
            return RedirectToAction("Album");

        }

        [HttpGet]
        public IActionResult Bands()
        {
            return View(dbManager_Bands.getBands());
        }


        [HttpGet]
        public IActionResult AggiungiBand()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AggiungiBand(Band band)
        {
            dbManager_Bands.aggiungiBand(band);
            return RedirectToAction("Bands");
        }

        [HttpGet]
        public IActionResult EliminaBand(int id)
        {
            var band = dbManager_Bands.getBands().Where(x => x.ID == id).FirstOrDefault();
            var delete = dbManager_Bands.eliminaBand(band);
            return RedirectToAction("Bands");

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