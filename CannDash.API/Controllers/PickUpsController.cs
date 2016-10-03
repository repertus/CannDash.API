using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CannDash.API.Infrastructure;
using CannDash.API.Models;

namespace CannDash.API.Controllers
{
    public class PickUpsController : ApiController
    {
        private CannDashDataContext db = new CannDashDataContext();

        // GET: api/PickUps
        public IQueryable<PickUp> GetPickUps()
        {
            return db.PickUps;
        }

        // GET: api/PickUps/5/5
        [ResponseType(typeof(PickUp))]
        [HttpGet, Route("api/pickups/{driverId}/{productId}")]
        public IHttpActionResult GetPickUp(int driverId, int productId)
        {
            PickUp pickUp = db.PickUps.Find(driverId, productId);
            if (pickUp == null)
            {
                return NotFound();
            }

            return Ok(pickUp);
        }

        // PUT: api/PickUps/5
        [ResponseType(typeof(void))]
        [HttpPut, Route("api/pickups/{driverId}/{productId}")]
        public IHttpActionResult PutPickUp(int driverId, int productId, PickUp pickUp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (driverId != pickUp.DriverId || productId != pickUp.ProductId)
            {
                return BadRequest();
            }

            db.Entry(pickUp).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PickUpExists(pickUp.DriverId,pickUp.ProductId))
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

        // POST: api/PickUps
        [ResponseType(typeof(PickUp))]
        public IHttpActionResult PostPickUp(PickUp pickUp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PickUps.Add(pickUp);

            try
            {
                db.SaveChanges();

                pickUp.Driver = db.Drivers.Find(pickUp.DriverId);
                pickUp.Product = db.Products.Find(pickUp.ProductId);
            }
            catch (DbUpdateException)
            {
                if (PickUpExists(pickUp.DriverId,pickUp.ProductId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = pickUp.DriverId }, pickUp);
        }

        // DELETE: api/PickUps/5
        [ResponseType(typeof(PickUp))]
        [HttpDelete, Route("api/pickups/{driverId}/{productId}")]
        public IHttpActionResult DeletePickUp(int driverId, int productId)
        {
            PickUp pickUp = db.PickUps.Find(driverId,productId);
            if (pickUp == null)
            {
                return NotFound();
            }

            db.PickUps.Remove(pickUp);
            db.SaveChanges();

            return Ok(pickUp);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PickUpExists(int driverId, int productId)
        {
            return db.PickUps.Count(e => e.DriverId == driverId && e.ProductId == productId) > 0;
        }
    }
}