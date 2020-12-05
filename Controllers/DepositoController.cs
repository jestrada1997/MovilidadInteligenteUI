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
    }
}
