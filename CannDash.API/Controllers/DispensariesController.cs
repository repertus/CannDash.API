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
    public class DispensariesController : ApiController
    {
        private CannDashDataContext db = new CannDashDataContext();

        // GET: api/Dispensaries
        public dynamic GetDispensaries()
        {
            return db.Dispensaries.Select(d => new
            { 
                d.DispensaryId,
                d.CompanyName,
                d.WeedMapMenu,
                d.Street,
                d.UnitNo,
                d.City,
                d.State,
                d.ZipCode,
                d.Email,
                d.Phone,
                d.Zone,
                d.StatePermit,
                d.PermitExpirationDate
            });
        }

        // GET: api/Dispensaries/5
        [ResponseType(typeof(Dispensary))]
        public IHttpActionResult GetDispensary(int id)
        {
            Dispensary dispensary = db.Dispensaries.Find(id);
            if (dispensary == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                dispensary.DispensaryId,
                dispensary.CompanyName,
                dispensary.WeedMapMenu,
                dispensary.Street,
                dispensary.UnitNo,
                dispensary.City,
                dispensary.State,
                dispensary.ZipCode,
                dispensary.Email,
                dispensary.Phone,
                dispensary.Zone,
                dispensary.StatePermit,
                dispensary.PermitExpirationDate
                
            });
        }

        // PUT: api/Dispensaries/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDispensary(int id, Dispensary dispensary)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dispensary.DispensaryId)
            {
                return BadRequest();
            }

            db.Entry(dispensary).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DispensaryExists(id))
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

        // POST: api/Dispensaries
        [ResponseType(typeof(Dispensary))]
        public IHttpActionResult PostDispensary(Dispensary dispensary)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Dispensaries.Add(dispensary);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = dispensary.DispensaryId }, dispensary);
        }

        // DELETE: api/Dispensaries/5
        [ResponseType(typeof(Dispensary))]
        public IHttpActionResult DeleteDispensary(int id)
        {
            Dispensary dispensary = db.Dispensaries.Find(id);
            if (dispensary == null)
            {
                return NotFound();
            }

            db.Dispensaries.Remove(dispensary);
            db.SaveChanges();

            return Ok(dispensary);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DispensaryExists(int id)
        {
            return db.Dispensaries.Count(e => e.DispensaryId == id) > 0;
        }
    }
}