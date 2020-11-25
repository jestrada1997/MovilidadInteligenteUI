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
        public async Task<IActionResult> Usuarios()
        {
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
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(usuario.idUsuario), "idUsuario");
                content.Add(new StringContent(usuario.nombre), "nombre");
                content.Add(new StringContent(usuario.apellido1), "apellido1");
                content.Add(new StringContent(usuario.apellido2), "apellido2");
                content.Add(new StringContent(usuario.correo), "correo");
                content.Add(new StringContent(usuario.clave), "clave");
                content.Add(new StringContent(usuario.telefono), "telefono");
                content.Add(new StringContent(usuario.saldo.ToString()), "saldo");
                content.Add(new StringContent(usuario.fechaCreacion.ToString()), "fechaCreacion");
                content.Add(new StringContent(usuario.rol), "rol");
                content.Add(new StringContent(usuario.estado.ToString()), "estado");
                StringContent data = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:44354/api/Usuario", data))
                {
                   
                    ViewBag.Result = "Success";
                }
            }
            return RedirectToAction("Usuarios", "Usuario", null);
        }

        //public async Task<IActionResult> Update(int id)
        //{
        //    Usuario Usuario = new Usuario();
        //    using (var httpClient = new HttpClient())
        //    {
        //        using (var response = await httpClient.GetAsync("https://localhost:44354/api/Usuario" + id.ToString()))
        //        {
        //            string apiResponse = await response.Content.ReadAsStringAsync();
        //            StringContent content = new StringContent(JsonConvert.SerializeObject(Usuario), Encoding.UTF8, "application/json");
        //        }
        //    }
        //    return View(Usuario);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Update(int id, Usuario Usuario)
        //{
        //    using (var httpClient = new HttpClient())
        //    {
        //        var request = new HttpRequestMessage
        //        {
        //            RequestUri = new Uri("https://localhost:44354/api/Usuario" + id),
        //            Method = new HttpMethod("Patch"),
        //            Content = new StringContent("[{ \"op\":\"replace\", \"path\":\"Nombre\", \"value\":\"" + Usuario.nombre + "\"},{ \"op\":\"replace\", \"path\":\"Apellido1\", \"value\":\"" + Usuario.apellido1 + "\"}]", Encoding.UTF8, "application/json")
        //        };

        //        var response = await httpClient.SendAsync(request);
        //    }
        //    return RedirectToAction("Usuarios");
        //}

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
    }
}
