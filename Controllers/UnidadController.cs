﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDB.Bson.Serialization;
using MovilidadInteligenteUI.Models;
using Newtonsoft.Json;

namespace MovilidadInteligenteUI.Controllers
{
    public class UnidadController : Controller
    {
        public async Task<IActionResult> Unidades()
        {
            if (UsuarioController.UserRol == "Cliente")
            {
                return RedirectToAction("Perfil", "Usuario");
            }
            List<Unidad> UnidadesList = new List<Unidad>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44354/api/Unidad"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    UnidadesList = BsonSerializer.Deserialize<List<Unidad>>(apiResponse);

                }
            }
            return View(UnidadesList);
        }



        public ViewResult Crear() => View();

        [HttpPost]
        public async Task<IActionResult> Crear(Unidad unidad)
        {
            Unidad receivedUsuario = new Unidad();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(unidad), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44354/api/Unidad", content))
                {

                }
            }
            return RedirectToAction("Unidades", "Unidad", null);
        }


        //[HttpPost]
        public async Task<IActionResult> GetUnidad(string id)
        {
            Unidad unidad = new Unidad();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44354/api/Unidad" + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    unidad = JsonConvert.DeserializeObject<Unidad>(apiResponse);
                }
            }
            return View(unidad);
        }

        public async Task<IActionResult> Update(string id)
        {
            Unidad Unidad = new Unidad();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44354/api/Unidad" + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    Unidad = JsonConvert.DeserializeObject<Unidad>(apiResponse);
                }
            }
            return View(Unidad);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Unidad Unidad)
        {
            Unidad receivedUnidad = new Unidad();
            using (var httpClient = new HttpClient())
            {

                StringContent data = new StringContent(JsonConvert.SerializeObject(Unidad), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("https://localhost:44354/api/Unidad", data))
                {
                    ViewBag.Result = "Unidad Actualizada";
                }


            }
            return View(Unidad);
        }


        public async Task<IActionResult> Borrar(string id)
        {
            Unidad unidad = new Unidad();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44354/api/Unidad" + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    unidad = JsonConvert.DeserializeObject<Unidad>(apiResponse);
                }
            }
            return View(unidad);
        }

        public async Task<IActionResult> Delete(string id)
        {

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44354/api/Unidad" + "?id=" + id))
                {

                }
            }

            return RedirectToAction("Unidades");
        }


        public async Task<IActionResult> GetDestino()
        {
           
            List<Unidad> UnidadesList = new List<Unidad>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44354/api/Unidad"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    UnidadesList = BsonSerializer.Deserialize<List<Unidad>>(apiResponse);

                }
            } 
          
            return View(UnidadesList);
        }


    }
}
