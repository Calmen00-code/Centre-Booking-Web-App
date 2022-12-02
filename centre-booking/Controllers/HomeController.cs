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

        [HttpPost]
        public IActionResult IsAuthenticate([FromBody] UserAPI user)
        {
            RestClient restClient = new RestClient(APPLICATION_LAYER_URL);
            // DEBUG
            System.Diagnostics.Debug.WriteLine("Username: " + user.Name + ", Password: " + user.Password);
            RestRequest authenticateRequest = new RestRequest("api/centre-application-layer/authenticate", Method.Post);

            authenticateRequest.AddJsonBody(user);
            
            RestResponse authenticateResponse = restClient.Execute(authenticateRequest);
            if (authenticateResponse.IsSuccessful) 
            {
                System.Diagnostics.Debug.WriteLine("Successful authenticate request");
                // return Ok(JsonConvert.DeserializeObject<string>(authenticateResponse.Content));
                return Ok(); // this does not work either
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("BadRequest authenticate request");
                return BadRequest(JsonConvert.DeserializeObject<string>(authenticateResponse.Content));
            }
        }

        [HttpPost]
        public IActionResult SomethingPost([FromBody] UserAPI user)
        {
            return Ok();
        }

        public IActionResult Something([FromBody] UserAPI user)
        {
            return Ok();
        }
    }
}
