using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson.Serialization;
using MovilidadInteligenteUI.Models;
using Newtonsoft.Json;

namespace MovilidadInteligenteUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public  IActionResult Index()
        {
            if (UsuarioController.UserRol == "Cliente")
            {
                return RedirectToAction("Perfil", "Usuario");
            }
            if (UsuarioController.UserRol == "Administrador")
            {
                return RedirectToAction("Usuarios", "Usuario");
            }
            return RedirectToAction("login", "Login", null);
        }

        public IActionResult Privacidad()
        {
            if (UsuarioController.UserRol == "Administrador")
            {
                return RedirectToAction("Usuarios", "Usuario");
            }
            return View();
        }
        public IActionResult SobreNosotros()
        {
            if (UsuarioController.UserRol == "Administrador")
            {
                return RedirectToAction("Usuarios", "Usuario");
            }
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
