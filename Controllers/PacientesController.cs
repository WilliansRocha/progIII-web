using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pacientesPessoas.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Uri = System.Uri;

namespace pacientesPessoas.Controllers
{
    public class PacientesController : Controller
    {
        private readonly ILogger<PacientesController> logger;
        private readonly HttpClient client;

        public PacientesController(ILogger<PacientesController> logger){
            this.logger = logger;
            client = new HttpClient{
                BaseAddress = new Uri("http://localhost:5000/api/")
            };
        }

        public IActionResult Index(){
            var responseString = GetPacientes().Result;

             //criar uma lista de pacientes:
            var listaPacientes = JsonConvert.DeserializeObject<IEnumerable<Pacientes>>(responseString);

            return View(listaPacientes);
        }

        async Task<string> GetPacientes(){

            HttpResponseMessage response = await client.GetAsync("pacientes");

            string responseBody = await response.Content.ReadAsStringAsync();
            
            return responseBody;
        }

        public IActionResult Editar(){
            return View();
        }

        public IActionResult Excluir(){
            return View();
        }

        
         [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}