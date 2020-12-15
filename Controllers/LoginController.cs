using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization;
using MovilidadInteligenteUI.Models;
using MovilidadInteligenteUI.Tools;
using Newtonsoft.Json;

namespace MovilidadInteligenteUI.Controllers
{
    public class LoginController : Controller
    {
        Usuario usuario = new Usuario();

        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Login(Login login, string returnUrl)
        {
            //var SecretKey = ConfigurationManager.AppSettings["SecretKey"];
            try
            {
                if (ModelState.IsValid)
                {
                    //var ClaveSegura = Seguridad.EncryptString(SecretKey, login.Clave);
                    //var claveoculta= Seguridad.DecryptString(SecretKey, ClaveSegura);


                    List<Usuario> UsuariosList = new List<Usuario>();
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.GetAsync("https://localhost:44354/api/Usuario"))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();

                            UsuariosList = BsonSerializer.Deserialize<List<Usuario>>(apiResponse);

                        }

                        
                    }
                    if (login != null)
                    {
                        foreach (var LoginUser in UsuariosList)
                        {
                            if (LoginUser.correo == login.Correo && LoginUser.clave == login.Clave && LoginUser.rol == "Administrador")
                            {
                                return RedirectToAction("Usuarios", "Usuario");

                            }


                            if (LoginUser.correo == login.Correo && LoginUser.clave == login.Clave && LoginUser.rol == "Cliente")
                            {
                                return RedirectToAction("Perfil", "Usuario", new { id = LoginUser.idUsuario });

                            }
                        }
                        return View(login);
                    }



                    //else
                    //{

                    //    var claims = new List<Claim>
                    //        {
                    //            new Claim(ClaimTypes.Name, login.Correo),
                    //            new Claim(ClaimTypes.Role, "Administrator"),
                    //        };

                    //    var claimsIdentity = new ClaimsIdentity(
                    //        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    //    var authProperties = new AuthenticationProperties
                    //    {
                    //        //AllowRefresh = <bool>,
                    //        // Refreshing the authentication session should be allowed.

                    //        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    //        // The time at which the authentication ticket expires. A 
                    //        // value set here overrides the ExpireTimeSpan option of 
                    //        // CookieAuthenticationOptions set with AddCookie.

                    //        IsPersistent = true,
                    //        // Whether the authentication session is persisted across 
                    //        // multiple requests. When used with cookies, controls
                    //        // whether the cookie's lifetime is absolute (matching the
                    //        // lifetime of the authentication ticket) or session-based.

                    //        //IssuedUtc = <DateTimeOffset>,
                    //        // The time at which the authentication ticket was issued.

                    //        RedirectUri = ""
                    //        // The full path or absolute URI to be used as an http 
                    //        // redirect response value.
                    //    };

                    //     HttpContext.SignInAsync(
                    //        CookieAuthenticationDefaults.AuthenticationScheme,
                    //        new ClaimsPrincipal(claimsIdentity),
                    //        authProperties);


                    //    if (authProperties.IsPersistent)
                    //    {

                    //    }



                    //}
                    return View(login);
                }
                else
                {
                    return View(login);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public ViewResult Registrar() => View();

        [HttpPost]
        public async Task<IActionResult> Registrar(Usuario usuario)
        {
            usuario.saldo = 0;
            usuario.saldoPend = 0;
            usuario.idUsuario = null;
            usuario.rol = "Cliente";
            usuario.estado = true;
            usuario.fechaCreacion = DateTime.Now;
            Usuario receivedUsuario = new Usuario();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44354/api/Usuario", content))
                {

                }
            }
            return RedirectToAction("login", "Login",null);
        }
    }
}
