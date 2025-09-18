using Microsoft.AspNetCore.Mvc;
using NZWalksUI.Models;
using System.Text;
using System.Text.Json;

namespace NZWalksUI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RegionsDTO> resp = new List<RegionsDTO>();
            try
            {
                //to get all regions from Regions controller of web api 
                var client = httpClientFactory.CreateClient();

                var httpRespMessage = await client.GetAsync("http://localhost:5125/api/Regions");

                httpRespMessage.EnsureSuccessStatusCode();

                //Old way of getting Response as string and using to pass to view with or without serializing
                //var resp = await httpResp.Content.ReadAsStringAsync();
                //ViewBag.Response = resp;

                resp.AddRange (await httpRespMessage.Content.ReadFromJsonAsync<IEnumerable<RegionsDTO>>());

            }
            catch (Exception)
            {

                throw;
            }

            return View(resp);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRegionModel addRegion)
        {
            var client = httpClientFactory.CreateClient();

            var httpReq = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("http://localhost:5125/api/Regions"),
                Content = new StringContent(JsonSerializer.Serialize(addRegion), Encoding.UTF8, "application/json")
            };

            var httpRespMessage = await client.SendAsync(httpReq);
            httpRespMessage.EnsureSuccessStatusCode ();
            
            var resp = await httpRespMessage.Content.ReadFromJsonAsync<RegionsDTO > ();

            if(resp is not null){
                return RedirectToAction("Index", "Regions");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = httpClientFactory.CreateClient();

            var resp = await client.GetFromJsonAsync<RegionsDTO>($"http://localhost:5125/api/Regions/{id.ToString()}");
            
            if(resp is not null) {
                return View(resp);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RegionsDTO request)
        {
            var client = httpClientFactory.CreateClient();

            var httpReq = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"http://localhost:5125/api/Regions/{request.ID.ToString()}"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };

            var httpRespMesg = await client.SendAsync(httpReq);
            httpRespMesg.EnsureSuccessStatusCode ();

            var resp = await httpRespMesg.Content.ReadFromJsonAsync<RegionsDTO>();

            if (resp is not null)
            {
                return RedirectToAction("Edit", "Regions");
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Delete(RegionsDTO request)
        {
            var client = httpClientFactory.CreateClient();


            var httpRespMesg = await client.DeleteAsync($"http://localhost:5125/api/Regions/{request.ID.ToString()}");
            httpRespMesg.EnsureSuccessStatusCode();

            return RedirectToAction("Index", "Regions");
        }
    }
}
