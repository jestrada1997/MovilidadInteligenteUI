using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization;
using MovilidadInteligenteUI.Models;
using Newtonsoft.Json;

namespace MovilidadInteligenteUI.Controllers
{
    public class BitacoraController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public void InsertarBitacora(string id) {
            Bitacora bitacora = new Bitacora();
            bitacora.accion = "Se realizo un deposito";
            bitacora.error = "no aplica";
            bitacora.fecha = DateTime.Now;
            bitacora.idUsuario = id;
            bitacora.estado = true;
            InsertarBitacora(bitacora);
        }

        public async Task<IActionResult> InsertarBitacora(Bitacora bitacora)
        {
          
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(bitacora), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44354/api/Bitacora", content))
                {

                }
            }

            return RedirectToAction("Depositos", "Deposito", null);
        }

        public void InsertarBitacoraPago(string id)
        {
            Bitacora bitacora = new Bitacora();
            bitacora.accion = "Se realizo un pago";
            bitacora.error = "no aplica";
            bitacora.fecha = DateTime.Now;
            bitacora.idUsuario = id;
            bitacora.estado = true;
            InsertarBitacoraPago(bitacora);
        }

        public async Task<IActionResult> InsertarBitacoraPago(Bitacora bitacora)
        {

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(bitacora), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44354/api/Bitacora", content))
                {

                }
            }

            return RedirectToAction("Depositos", "Deposito", null);
        }
    }
}
