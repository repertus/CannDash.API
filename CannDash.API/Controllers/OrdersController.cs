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
    public class OrdersController : ApiController
    {
        private CannDashDataContext db = new CannDashDataContext();

        // GET: api/Orders
        //Todo: authorize role for only admin
        public dynamic GetOrders()
        {
            return db.Orders.Select(o => new
            {
                o.OrderId,
                o.DispensaryId,
                o.DriverId,
                o.OrderDate,
                o.DeliveryNotes,
                o.PickUp,
                o.Street,
                o.UnitNo,
                o.City,
                o.State,
                o.ZipCode,
                o.ItemQuantity,
                o.TotalCost,
                o.OrderDelivered
            });
        }

        // GET: api/Orders/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult GetOrder(int id)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                order.OrderId,
                order.DispensaryId,
                order.DriverId,
                order.OrderDate,
                order.DeliveryNotes,
                order.PickUp,
                order.Street,
                order.UnitNo,
                order.City,
                order.State,
                order.ZipCode,
                order.ItemQuantity,
                order.TotalCost,
                order.OrderDelivered
            });
        }

        // PUT: api/Orders/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrder(int id, Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.OrderId)
            {
                return BadRequest();
            }

            db.Entry(order).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // POST: api/Orders
        [ResponseType(typeof(Order))]
        public IHttpActionResult PostOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Orders.Add(order);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = order.OrderId }, order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(int id)
        {
            return db.Orders.Count(e => e.OrderId == id) > 0;
        }
    }
}