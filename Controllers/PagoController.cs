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
    public class PagoController : Controller
    {
        public async Task<IActionResult> Pagos()
        {
            List<Pago> PagosList = new List<Pago>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44354/api/Pago"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    PagosList = BsonSerializer.Deserialize<List<Pago>>(apiResponse);

                }
            }
            return View(PagosList);
        }

       // public ViewResult Crear() => View();


        //[HttpPost]
        public async Task<IActionResult> GetPago(string id)
        {
            //Unidad unidad = new Unidad();
            Pago pago = new Pago();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44354/api/Pago" + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    pago = JsonConvert.DeserializeObject<Pago>(apiResponse);
                }
            }
            return View(pago);
        }

    }
}
