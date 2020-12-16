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
    public class UsuarioController : Controller
    {

        public static String UserGlobal;
        public static String UserRol;
        public static int cartera;
        public async Task<IActionResult> Usuarios()
        {
            if (UserRol== "Cliente") {
                return RedirectToAction("Perfil", "Usuario");
            }

            List<Usuario> UsuariosList = new List<Usuario>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44354/api/Usuario"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    UsuariosList = BsonSerializer.Deserialize<List<Usuario>>(apiResponse);

                }
            }
            return View(UsuariosList);
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //public ViewResult GetUsuario() => View();

        //[HttpPost]
        public async Task<IActionResult> GetUsuario(string id)
        {
            Usuario usuario = new Usuario();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44354/api/Usuario" + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    usuario = JsonConvert.DeserializeObject<Usuario>(apiResponse);
                }
            }
            return View(usuario);
        }


        public ViewResult Crear() => View();

        [HttpPost]
        public async Task<IActionResult> Crear(Usuario usuario)
        {         
            usuario.saldoPend = 0;
            usuario.idUsuario = null;
            usuario.fechaCreacion = DateTime.Now;
            Usuario receivedUsuario = new Usuario();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44354/api/Usuario", content))
                {
                    
                }
            }
            return RedirectToAction("Usuarios", "Usuario", null);
        }

       

        public async Task<IActionResult> Update(string id)
        {
            Usuario usuario = new Usuario();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44354/api/Usuario" +"/"+ id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    
                    usuario = JsonConvert.DeserializeObject<Usuario>(apiResponse);
                }
            }
            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Usuario usuario)
        {
            Usuario receivedUsuario = new Usuario();
            using (var httpClient = new HttpClient())
            {

                StringContent data = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("https://localhost:44354/api/Usuario", data))
                {

                    ViewBag.Result = "Usuario Actualizado";
                }


            }
            return View(usuario);
        }

        public async Task<IActionResult> Borrar(string id)
        {
            Usuario usuario = new Usuario();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44354/api/Usuario" + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    usuario = JsonConvert.DeserializeObject<Usuario>(apiResponse);
                }
            }
            return View(usuario);
        }

        
        public async Task<IActionResult> Delete(string id)
        {
            
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44354/api/Usuario"+ "?id=" + id))
                {
                    
                }
            }

            return RedirectToAction("Usuarios");
        }
        [Route("idUsuario")]
        public async Task<IActionResult> Perfil(string id)
        {
            if (UserGlobal == null) {
                UserGlobal = id; 
            }
            
            Usuario usuario = new Usuario();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44354/api/Usuario" + "/" + UserGlobal))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    usuario = JsonConvert.DeserializeObject<Usuario>(apiResponse);
                }
            }
            UserRol = usuario.rol;
            cartera = usuario.saldo;
            return View(usuario);
            
        }


        //************************************Cliente****************************************************


        public async Task<IActionResult> GetUsuarioCliente(string id)
        {
            Usuario usuario = new Usuario();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44354/api/Usuario" + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    usuario = JsonConvert.DeserializeObject<Usuario>(apiResponse);
                }
            }
            return View(usuario);
        }


        public async Task<IActionResult> UpdateCliente(string id)
        {
            Usuario usuario = new Usuario();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44354/api/Usuario" + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    usuario = JsonConvert.DeserializeObject<Usuario>(apiResponse);
                }
            }
            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCliente(Usuario usuario)
        {
            Usuario receivedUsuario = new Usuario();
            using (var httpClient = new HttpClient())
            {

                StringContent data = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("https://localhost:44354/api/Usuario", data))
                {

                    ViewBag.Result = "Usuario Actualizado";
                }


            }
            return View(usuario);
        }



        public async Task<IActionResult> Salir()
        {
           UserGlobal=null;
            UserRol=null;
            cartera = 0;
            return RedirectToAction("login", "Login", null);
        }

    }
}

