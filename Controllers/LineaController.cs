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
    public class LineaController : Controller
    {
        public async Task<IActionResult> Lineas()
        {
            List<Linea> LineasList = new List<Linea>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44354/api/Linea"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    LineasList = BsonSerializer.Deserialize<List<Linea>>(apiResponse);

                }
            }
            return View(LineasList);
        }

        
        public async Task<IActionResult> GetLinea(string id)
        {
            Linea Linea = new Linea();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44354/api/Linea" + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    Linea = JsonConvert.DeserializeObject<Linea>(apiResponse);
                }
            }
            return View(Linea);
        }

        public ViewResult Crear() => View();

        [HttpPost]
        public async Task<IActionResult> Crear(Linea Linea)
        {
            Linea receivedLinea = new Linea();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(Linea), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44354/api/Linea", content))
                {

                }
            }
            return RedirectToAction("Lineas", "Linea", null);
        }



        public async Task<IActionResult> Update(string id)
        {
            Linea Linea = new Linea();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44354/api/Linea" + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    Linea = JsonConvert.DeserializeObject<Linea>(apiResponse);
                }
            }
            return View(Linea);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Linea Linea)
        {
            Linea receivedLinea = new Linea();
            using (var httpClient = new HttpClient())
            {
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(Linea.idLinea), "idUsuario");
                content.Add(new StringContent(Linea.descripcion), "nombre");
                content.Add(new StringContent(Linea.monto.ToString()), "Monto");
                content.Add(new StringContent(Linea.codigoCTP.ToString()), "CodigoCTP");
                content.Add(new StringContent(Linea.estado.ToString()), "estado");
                StringContent data = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:44354/api/Linea", data))
                {

                    ViewBag.Result = "Success";
                }
            }
            return RedirectToAction("Lineas", "Linea", null);
        }


        public async Task<IActionResult> Borrar(string id)
        {
            Linea Linea = new Linea();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44354/api/Linea" + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    Linea = JsonConvert.DeserializeObject<Linea>(apiResponse);
                }
            }
            return View(Linea);
        }


        public async Task<IActionResult> Delete(string id)
        {

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44354/api/Linea" + "?id=" + id))
                {

                }
            }

            return RedirectToAction("Lineas");
        }
    }
}
