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
    public class DepositoController : Controller
    {
        public async Task<IActionResult> Depositos()
        {
            if (UsuarioController.UserRol == "Cliente")
            {
                return RedirectToAction("Perfil", "Usuario");
            }

            List<Deposito> DepositosList = new List<Deposito>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44354/api/Deposito"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    DepositosList = BsonSerializer.Deserialize<List<Deposito>>(apiResponse);

                }
            }
            return View(DepositosList);
        }

        public async Task<IActionResult> GetDeposito(string id)
        {
            Deposito Deposito = new Deposito();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44354/api/Deposito" + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    Deposito = JsonConvert.DeserializeObject<Deposito>(apiResponse);
                }
            }
            return View(Deposito);
        }

        public ViewResult Crear() => View();

        [HttpPost]
        public async Task<IActionResult> Crear(Deposito Deposito)
        {
            Deposito receivedLinea = new Deposito();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(Deposito), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44354/api/Deposito", content))
                {

                }
            }

            await this.UpdateClienteSaldo(UsuarioController.UserGlobal,Deposito.monto);

            return RedirectToAction("Depositos", "Deposito", null);
        }



        public async Task<IActionResult> Update(string id)
        {
            Deposito Deposito = new Deposito();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44354/api/Deposito" + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    Deposito = JsonConvert.DeserializeObject<Deposito>(apiResponse);
                }
            }
            return View(Deposito);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Deposito Deposito)
        {
            Deposito receivedLinea = new Deposito();
            using (var httpClient = new HttpClient())
            {

                StringContent data = new StringContent(JsonConvert.SerializeObject(Deposito), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("https://localhost:44354/api/Deposito", data))
                {

                }


            }
            return RedirectToAction("Depositos", "Deposito", null);
        }


        public async Task<IActionResult> Borrar(string id)
        {
            Deposito Deposito = new Deposito();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44354/api/Deposito" + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    Deposito = JsonConvert.DeserializeObject<Deposito>(apiResponse);
                }
            }
            return View(Deposito);
        }


        public async Task<IActionResult> Delete(string id)
        {

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44354/api/Deposito" + "?id=" + id))
                {

                }
            }

            return RedirectToAction("Depositos");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateClienteSaldo(string id, int Pago)
        {
            Usuario usuario = new Usuario();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44354/api/Usuario" + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    usuario = JsonConvert.DeserializeObject<Usuario>(apiResponse);
                    usuario.saldo = +Pago;

                }

                StringContent data = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("https://localhost:44354/api/Usuario", data))
                {

                    ViewBag.Result = "Usuario Actualizado";
                }
            }
            return View(usuario);
        }
    }
}
