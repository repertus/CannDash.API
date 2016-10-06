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
    public class CustomersController : ApiController
    {
        private CannDashDataContext db = new CannDashDataContext();

        // GET: api/Customers
        public dynamic GetCustomers()
        {
            return db.Customers.Select(c => new
            {
                c.CustomerId,
                c.DispensaryId,
                c.FirstName,
                c.LastName,
                c.Street,
                c.UnitNo,
                c.City,
                c.State,
                c.ZipCode,
                c.Email,
                c.Phone,
                c.Gender,
                c.DateOfBirth,
                c.Age,
                c.MedicalReason,
                c.DriversLicense,
                c.MmicId,
                c.MmicExpiration,
                c.DoctorLetter
            });
        }

        // GET: api/Customers/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult GetCustomer(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                customer.CustomerId,
                customer.DispensaryId,
                customer.FirstName,
                customer.LastName,
                customer.Street,
                customer.UnitNo,
                customer.City,
                customer.State,
                customer.ZipCode,
                customer.Email,
                customer.Phone,
                customer.Gender,
                customer.DateOfBirth,
                customer.Age,
                customer.MedicalReason,
                customer.DriversLicense,
                customer.MmicId,
                customer.MmicExpiration,
                customer.DoctorLetter
            });
        }

        // PUT: api/Customers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            var customerToBeUpdated = db.Customers.Find(id);

            db.Entry(customerToBeUpdated).CurrentValues.SetValues(customer);
            db.Entry(customerToBeUpdated).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // POST: api/Customers
        [ResponseType(typeof(Customer))]
        public IHttpActionResult PostCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Customers.Add(customer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = customer.CustomerId }, customer);
        }

        // DELETE: api/Customers/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult DeleteCustomer(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            db.Customers.Remove(customer);
            db.SaveChanges();

            return Ok(customer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerExists(int id)
        {
            return db.Customers.Count(e => e.CustomerId == id) > 0;
        }
    }
}