using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace centre_booking.Controllers
{
    public class HomeController : Controller
    {
        public static readonly string APPLICATION_LAYER_URL = "http://localhost:51116/";

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult IsAuthenticate(string username, string password)
        {
            RestClient restClient = new RestClient(APPLICATION_LAYER_URL);
            RestRequest authenticateRequest = new RestRequest("api/centre-application-layer/authenticate", Method.Post);

            UserAPI user = new UserAPI();
            user.Name = username;
            user.Password = password;
            authenticateRequest.AddJsonBody(user);
            
            RestResponse authenticateResponse = restClient.Execute(authenticateRequest);
            if (authenticateResponse.IsSuccessful) 
            {
                return Ok(JsonConvert.DeserializeObject<string>(authenticateResponse.Content));
            }
            else
            {
                return BadRequest(JsonConvert.DeserializeObject<string>(authenticateResponse.Content));
            }
        }
    }
}
