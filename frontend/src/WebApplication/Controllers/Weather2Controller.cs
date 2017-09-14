using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication.Controllers
{
    public class Weather2Controller : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewData["City"] = "testcity";

            return View();
        }

		[HttpGet("[action]/{city}")]
		public async Task<IActionResult> City(string city)
		{
			using (var client = new HttpClient())
			{
				try
				{
					client.BaseAddress = new Uri("http://api.openweathermap.org");
					var response = await client.GetAsync($"/data/2.5/weather?q={city}&appid=226aa16a254ec93015b60f8fe450d9b3&units=metric");
					response.EnsureSuccessStatusCode();

					var stringResult = await response.Content.ReadAsStringAsync();
					var rawWeather = JsonConvert.DeserializeObject<OpenWeatherResponse>(stringResult);


                    /*return Ok(new
					{
						Temp = rawWeather.Main.Temp,
						Summary = string.Join(",", rawWeather.Weather.Select(x => x.Main)),
						City = rawWeather.Name
					});*/

					ViewData["City"] = rawWeather.Name;
                    ViewData["Temp"] = rawWeather.Main.Temp;
                    ViewData["Summary"] = string.Join(",", rawWeather.Weather.Select(x => x.Main));

					return View("Index");
				}
				catch (HttpRequestException httpRequestException)
				{
					return BadRequest($"Error getting weather from OpenWeather: {httpRequestException.Message}");
				}
			}
		}


    }

	
}
