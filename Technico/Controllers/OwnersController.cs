using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Technico.Data;
using TechnicoWebApi.Dtos;
using TechnicoWebApi.Models;

namespace Technico.Controllers
{
    public class OwnersController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HttpClient httpClient = new HttpClient();
            string url = "http://localhost:5012/api/Owner";
            // Await the response to complete the asynchronous task
            var response = await httpClient.GetAsync(url);

            if(response.IsSuccessStatusCode)
            {
                // Await the content to get the JSON string result
                var jsonResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine(jsonResponse);

                // Deserialize the JSON string to a list of Owner objects
                var ownerList = JsonConvert.DeserializeObject<List<Owner>>(jsonResponse);

                // Pass the ownerList to the View
                return View(ownerList);
            }
            else
            {
                // Handle the error case, e.g., log it or return an error view
                Console.WriteLine("Error: Unable to retrieve data.");
                return View("Error");
            }
        }
    }
}
