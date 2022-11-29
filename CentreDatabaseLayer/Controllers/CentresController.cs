using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Web.Http;
using System.Web.Http.Description;
using CentreDatabaseLayer.Models;

namespace CentreDatabaseLayer.Controllers
{
    [RoutePrefix("api/centres")]
    public class CentresController : ApiController
    {
        private CentreDatabaseEntities db = new CentreDatabaseEntities();
        public static readonly string ADMIN_USERNAME = "admin";
        public static readonly string ADMIN_PASS = "adminpass";

        [Route("")]
        public List<Centre> GetCentres()
        {
            List<Centre> centres = db.Centres.ToList();
            return centres;
        }

        [Route("{centreName}")]
        [ResponseType(typeof(Centre))]
        
        public IHttpActionResult GetCentre(string centreName)
        {
            Centre centre = db.Centres.Find(centreName);
            if (centre == null)
            {
                return NotFound();
            }

            return Ok(centre);
        }

        [Route("{centreName}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCentre(string centreName, [FromBody] Centre centre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (centreName != centre.CentreName)
            {
                return BadRequest();
            }

            db.Entry(centre).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CentreExists(centreName))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("")]
        [ResponseType(typeof(Centre))]
        public IHttpActionResult PostCentre([FromBody] Centre centre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Centres.Add(centre);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CentreExists(centre.CentreName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            CreatedAtRoute("DefaultApi", new { id = centre.CentreName }, centre);
            return Ok(centre);
        }

        [Route("{centreName}")]
        [ResponseType(typeof(Centre))]
        public IHttpActionResult DeleteCentre(string centreName)
        {
            Centre centre = db.Centres.Find(centreName);
            if (centre == null)
            {
                return NotFound();
            }

            db.Centres.Remove(centre);
            db.SaveChanges();

            return Ok(centre);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CentreExists(string id)
        {
            return db.Centres.Count(e => e.CentreName == id) > 0;
        }
    }
}