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
    public class AccessRequestController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewData["City"] = "EARS City";

            return View();
        }

		[HttpGet("[action]/{id}")]
		public async Task<IActionResult> Access(string id)
		{
			using (var client = new HttpClient())
			{
				try
				{
					/*client.BaseAddress = new Uri("http://api.openweathermap.org");
                    var response = await client.GetAsync($"/data/2.5/weather?q={city}&appid=226aa16a254ec93015b60f8fe450d9b3&units=metric");                    
                    */
					client.BaseAddress = new Uri("https://accessmanagement.app.cloud.gov");
					var response = await client.GetAsync($"/ears/v0/accessrequests/{id}");

					response.EnsureSuccessStatusCode();

					var stringResult = await response.Content.ReadAsStringAsync();
					var rawAccessRequest = JsonConvert.DeserializeObject<AccessRequestAPIResponse>(stringResult);


					/*return Ok(new
                    {
                        Temp = rawWeather.Main.Temp,
                        Summary = string.Join(",", rawWeather.Weather.Select(x => x.Main)),
                        City = rawWeather.Name
                    });*/

					ViewData["id"] = rawAccessRequest.Id;
					ViewData["sample_Field_1"] = rawAccessRequest.Sample_Field_1;
					ViewData["sample_Field_2"] = rawAccessRequest.Sample_Field_2;

					return View("Index");
				}
				catch (HttpRequestException httpRequestException)
				{
					return BadRequest($"Error getting weather from OpenWeather: {httpRequestException.Message}");
				}
			}
		}


		[HttpGet("[action]")]
		public async Task<IActionResult> Access()

		{
			using (var client = new HttpClient())
			{
				try
				{
					/*client.BaseAddress = new Uri("http://api.openweathermap.org");
					var response = await client.GetAsync($"/data/2.5/weather?q={city}&appid=226aa16a254ec93015b60f8fe450d9b3&units=metric");                    
                    */
                    client.BaseAddress = new Uri("https://accessmanagement.app.cloud.gov");
                    var response = await client.GetAsync($"/ears/v0/accessrequests");

                    response.EnsureSuccessStatusCode();

					var stringResult = await response.Content.ReadAsStringAsync();
					var rawAccessRequestList = JsonConvert.DeserializeObject<List<AccessRequestAPIResponse>>(stringResult);


                    /*return Ok(new
					{
						Temp = rawWeather.Main.Temp,
						Summary = string.Join(",", rawWeather.Weather.Select(x => x.Main)),
						City = rawWeather.Name
					});*/

					/*ViewData["id"] = rawAccessRequestList.AccessRequests.ElementAt(0).Id;
                    ViewData["sample_Field_1"] = rawAccessRequestList.AccessRequests.ElementAt(0).Sample_Field_1;
					ViewData["sample_Field_2"] = rawAccessRequestList.AccessRequests.ElementAt(0).Sample_Field_2;
                    */
					return View("Requests", rawAccessRequestList);
				}
				catch (HttpRequestException httpRequestException)
				{
					return BadRequest($"Error getting weather from OpenWeather: {httpRequestException.Message}");
				}
			}
		}


    }
	public class AccessRequestAPIResponseList
	{

        public List<AccessRequestAPIResponse> AccessRequests { get; set; }
	}


	public class AccessRequestAPIResponse
	{
		public string Id { get; set; }
		public string Sample_Field_1 { get; set; }
        public string Sample_Field_2 { get; set; }
	}

	
}
