using gestionServiciosVirtuales.Models;
using Microsoft.AspNetCore.Mvc;
using pruebaUsuario.Data;
using System.Collections.Generic;
using System.Diagnostics;

namespace pruebaUsuario.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

       private AppDbContext _db;

        public HomeController(ILogger<HomeController> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {

            //  List<AspNetUser> users = _db.AspNetUsers.ToList();
            return Redirect("/servicios");
        }

      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult filtrarPorEstado(int? id)
        {
            if (id == 1) 
            { 
                
            }

            return View();
        }
    }
}