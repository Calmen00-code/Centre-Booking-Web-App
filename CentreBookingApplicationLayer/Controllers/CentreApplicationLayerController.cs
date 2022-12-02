using APIClasses;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace CentreBookingApplicationLayer.Controllers
{
    [RoutePrefix("api/centre-application-layer")]
    public class CentreApplicationLayerController : ApiController
    {
        private static readonly string CENTRE_DATABASE_URL = "http://localhost:57907/";

        [Route("centres")]
        [HttpGet]
        public IHttpActionResult GetCentres()
        {
            RestClient restClient = new RestClient(CENTRE_DATABASE_URL);
            RestRequest centresRequest = new RestRequest("api/centres", Method.Get);
            RestResponse centresResponse = restClient.Execute(centresRequest);

            if (centresResponse.IsSuccessful)
            {
                return Ok(JsonConvert.DeserializeObject<List<CentreAPI>>(centresResponse.Content));
            }
            else
            {
                return InternalServerError();
            }
        }

        [Route("{centreName}")]
        [HttpGet]
        public IHttpActionResult GetCentre(string centreName)
        {
            RestClient restClient = new RestClient(CENTRE_DATABASE_URL);
            RestRequest centresRequest = new RestRequest("api/centres/{centreName}", Method.Get);
            centresRequest.AddUrlSegment("centreName", centreName);
            RestResponse centresResponse = restClient.Execute(centresRequest);

            if (centresResponse.IsSuccessful)
            {
                return Ok(JsonConvert.DeserializeObject<CentreAPI>(centresResponse.Content));
            }
            else
            {
                return Content(HttpStatusCode.NotFound, "Centre given not found!");
            }
        }

        [Route("update-centre/{centreName}")]
        [HttpPut]
        public IHttpActionResult UpdateCentre(string centreName, [FromBody] CentreAPI centre)
        {
            RestClient restClient = new RestClient(CENTRE_DATABASE_URL);
            RestRequest updateRequest = new RestRequest("api/centres/{centreName}", Method.Put);
            updateRequest.AddUrlSegment("centreName", centreName);
            updateRequest.AddJsonBody(centre);
            RestResponse updateResponse = restClient.Execute(updateRequest);

            if (updateResponse.IsSuccessful)
            {
                return Ok();
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, "Something went wrong while updating centre");
            }
        }

        [Route("add-centre")]
        [HttpPost]
        public IHttpActionResult AddCentre([FromBody] CentreAPI centre)
        {
            RestClient restClient = new RestClient(CENTRE_DATABASE_URL);
            RestRequest addRequest = new RestRequest("api/centres", Method.Post);
            addRequest.AddJsonBody(centre);
            RestResponse addResponse = restClient.Execute(addRequest);

            if (addResponse.IsSuccessful)
            {
                return Ok(JsonConvert.DeserializeObject<CentreAPI>(addResponse.Content));
            }
            else
            {
                // TODO: implement to differentiate between bad request and conflict
                return Content(HttpStatusCode.BadRequest, "Something went wrong while adding centre");
            }
        }

        [Route("delete-centre/{centreName}")]
        [HttpDelete]
        public IHttpActionResult DeleteCentre(string centreName)
        {
            RestClient restClient = new RestClient(CENTRE_DATABASE_URL);
            RestRequest deleteRequest = new RestRequest("api/centres/{centreName}", Method.Delete);
            deleteRequest.AddUrlSegment("centreName", centreName);
            RestResponse deleteResponse = restClient.Execute(deleteRequest);

            if (deleteResponse.IsSuccessful)
            {
                return Ok(JsonConvert.DeserializeObject<CentreAPI>(deleteResponse.Content));
            }
            else
            {
                return Content(HttpStatusCode.NotFound, "Centre given is unregistered in the system!");
            }
        }

        [Route("authenticate")]
        [HttpPost]
        public IHttpActionResult Authenticate([FromBody] UserAPI user)
        {
            RestClient restClient = new RestClient(CENTRE_DATABASE_URL);
            RestRequest authenticateRequest = new RestRequest("api/centres/authenticate", Method.Post);
            // DEBUG
            System.Diagnostics.Debug.WriteLine("UserAPI name: " + user.Name + ", UserAPI pass: " + user.Password);
            authenticateRequest.AddJsonBody(user);
            RestResponse authenticateResponse = restClient.Execute(authenticateRequest);

            if (authenticateResponse.IsSuccessful)
            {
                return Ok("Welcome Admin!");
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, "User unauthorised!");
            }
        }
    }
}
