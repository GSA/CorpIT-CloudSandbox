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

					ViewData["id"] = rawAccessRequest.Id;
					ViewData["sample_Field_1"] = rawAccessRequest.Sample_Field_1;
					ViewData["sample_Field_2"] = rawAccessRequest.Sample_Field_2;

					//call the new API
					
					//TODO: run this on cloud.gov
					
					var client2 = new HttpClient();
					
					//client2.BaseAddress = new Uri("http://localhost:3000");
					client2.BaseAddress = new Uri("https://mock-ea.app.cloud.gov");
					var eaResponse = await client2.GetAsync($"/api/v0/applications/99/pocs");
					
					eaResponse.EnsureSuccessStatusCode();

					var eaStringResult = await eaResponse.Content.ReadAsStringAsync();
					var EArawAccessRequest = JsonConvert.DeserializeObject<List<EAPOCResponse>>(eaStringResult);

					ViewData["EAName"] = EArawAccessRequest[0].Name;
					ViewData["EAEmail"] = EArawAccessRequest[0].Email;
					



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
					
                    
					return View("Requests", rawAccessRequestList);
				}
				catch (HttpRequestException httpRequestException)
				{
					return BadRequest($"Error getting weather from OpenWeather: {httpRequestException.Message}");
				}
			}
		}

        [HttpGet("[action]")]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Sample_Field_1,Sample_Field_2")] AccessRequestPost request)
        {
        if (ModelState.IsValid)
        {
            using (var client = new HttpClient())
            {
                    try
                    {
                        string json = JsonConvert.SerializeObject(request);
                        HttpContent content = new StringContent(json);
                        content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                        client.BaseAddress = new Uri("https://accessmanagement.app.cloud.gov");
                        var response = await client.PostAsync($"/ears/v0/accessrequests", content);

                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Access");
                        }
                        else
                        {
                            return BadRequest($"Error creating access request: {response.RequestMessage}");
                        }
                }
                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest($"Error creating access request: {httpRequestException.Message}");
                }
            }
        }
        else
        {
                return BadRequest($"Error creating access request: invalid model state");
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

	public class AccessRequestPost
	{
		public string Sample_Field_1 { get; set; }
		public string Sample_Field_2 { get; set; }
	}

	public class EAPOCResponse
	{
		
		   /* 
	  {
    "ParentId": 36580,
    "Name": "James Monroe",
    "Phone": null,
    "Email": "James.Monroe@gsa.gov",
    "Owner": null,
    "Type": "Business"
  }*/
		
		public string ParentId { get; set; }
		public string Name { get; set; }
        public string Phone { get; set; }
		public string Email { get; set; }
		public string Owner { get; set; }
		public string Type { get; set; }

	}
	
	
	
}
