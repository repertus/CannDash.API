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
                o.DispensaryOrderNo,
                o.DriverId,
                o.CustomerId,
                o.CustomerAddressId,
                o.OrderDate,
                o.DeliveryNotes,
                o.PickUp,
                o.Street,
                o.UnitNo,
                o.City,
                o.State,
                o.ZipCode,
                o.MenuCategoryId,
                o.CategoryName,
                o.ProductId,
                o.ProductName,
                o.ItemQuantity,
                o.TotalCost,
                o.OrderStatus
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
                    order.CustomerId,
                    order.CustomerAddressId,
                    CustomerInfo = new
                    {
                        order.Customer.FirstName,
                        order.Customer.LastName,
                        order.Customer.Email,
                        order.Customer.Phone
                    },
                    ProductOrder = order.ProductOrders.Select(p => new
                    {
                        p.Discount,
                        p.OrderId,
                        p.ProductId,
                        p.ProductOrderId,
                        p.TotalSale,
                        p.UnitPrice
                    }),
                    order.OrderDate,
                    order.DeliveryNotes,
                    order.PickUp,
                    order.Street,
                    order.UnitNo,
                    order.City,
                    order.State,
                    order.ZipCode,
                    order.MenuCategoryId,
                    order.CategoryName,
                    order.ProductId,
                    order.ProductName,
                    order.ItemQuantity,
                    order.TotalCost,
                    order.OrderStatus
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

            order.OrderDate = DateTime.Now;
            order.OrderStatus = 1;
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