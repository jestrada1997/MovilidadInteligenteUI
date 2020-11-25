using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MovilidadInteligenteUI.Controllers
{
    public class UnidadController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
