﻿using System;
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
            if (UsuarioController.UserRol == "Cliente")
            {
                return RedirectToAction("Perfil", "Usuario");
            }


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

        public  IActionResult CrearPago(string id, int monto) {
            Pago Pago = new Pago();
            Pago.idUsuario = UsuarioController.UserGlobal;
            Pago.idUnidad = id;
            Pago.monto = monto;
            Pago.fechaPago = DateTime.Now;
            Pago.estado = true;

           
            Crear(Pago);
            
          
            if (monto>UsuarioController.cartera)
            {
                ViewData["usuario"] = "No tiene suficientes fondos";
                return RedirectToAction("Crear", "Deposito", null);
            }

            ViewData["usuario"] = "Agendado";
            return RedirectToAction("Perfil", "Usuario", null);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Pago Pago)
        {
            
            Pago.idUsuario = UsuarioController.UserGlobal;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(Pago), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44354/api/Pago", content))
                {

                }
            }
            await this.UpdateClienteSaldo(UsuarioController.UserGlobal, Pago.monto);


            return RedirectToAction("Perfil", "Usuario", null);

        }

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

        public async Task<IActionResult> Update(string id)
        {
            Pago Pago = new Pago();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44354/api/Pago" + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    Pago = JsonConvert.DeserializeObject<Pago>(apiResponse);
                }
            }
            return View(Pago);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Pago Pago)
        {
            Pago receivedLinea = new Pago();
            using (var httpClient = new HttpClient())
            {

                StringContent data = new StringContent(JsonConvert.SerializeObject(Pago), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("https://localhost:44354/api/Pago", data))
                {

                }


            }
            return RedirectToAction("Pagos", "Pago", null);
        }


        public async Task<IActionResult> Borrar(string id)
        {
            Pago Pago = new Pago();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44354/api/Pago" + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    Pago = JsonConvert.DeserializeObject<Pago>(apiResponse);
                }
            }
            return View(Pago);
        }


        public async Task<IActionResult> Delete(string id)
        {

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44354/api/Pago" + "?id=" + id))
                {

                }
            }

            return RedirectToAction("Pagos");
        }



        //*************************************************CLIENTE*******************************************************

        public ViewResult CrearCliente() => View();

        [HttpPost]
        public async Task<IActionResult> CrearCliente(Pago Pago)
        {
            Pago receivedLinea = new Pago();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(Pago), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44354/api/Pago", content))
                {

                }
            }
            return RedirectToAction("Pagos", "Pago", null);
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
                    if (Pago>usuario.saldo) {
                        return RedirectToAction("Crear", "Deposito", null);
                    }
                    else { 
                        usuario.saldo = usuario.saldo - Pago;
                        BitacoraController bb = new BitacoraController();
                        bb.InsertarBitacoraPago(id);
                    }
                   

                }

                StringContent data = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("https://localhost:44354/api/Usuario", data))
                {

                    ViewBag.Result = "Usuario Actualizado";
                }
            }
            return View(usuario);
        }

        public async Task<IActionResult> HistorialPagos(string id)
        {
            List<Pago> PagosList = new List<Pago>();
            ViewData["idUsuario"]= id;
            Usuario usuario = new Usuario();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44354/api/Pago"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    
                    PagosList = BsonSerializer.Deserialize<List<Pago>>(apiResponse);

                }
                using (var response = await httpClient.GetAsync("https://localhost:44354/api/Usuario" + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    usuario = JsonConvert.DeserializeObject<Usuario>(apiResponse);
                }
            }
            ViewData["NombreUsuario"] = usuario.nombre;
            ViewData["Ruta"] = usuario.nombre;
            return View(PagosList);
        }
    }
}
