using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovilidadInteligenteUI.Models;

namespace MovilidadInteligenteUI.Controllers
{
    public class LoginController : Controller
    {
        Usuario usuario = new Usuario();

        public ActionResult Login()
        {
            return View();
        }
    }
}
