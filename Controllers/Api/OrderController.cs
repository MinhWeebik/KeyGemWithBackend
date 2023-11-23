using KeyGem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KeyGem.Controllers.Api
{
    public class OrderController : ApiController
    {
        private ApplicationDbContext _context;

        public OrderController()
        {
            _context = new ApplicationDbContext();
        }
        public IHttpActionResult GetOrder()
        {
            var orderData = _context.Order.Include(o => o.Variant).Include(o => o.Person).Include(o => o.Product).Include(o => o.OrderState).ToList();
            return Ok(orderData);
        }

        [HttpPost]
        public IHttpActionResult CreateOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var variant = _context.Variant.Single(x => x.Id == order.VariantId);
            var product = _context.Product.Single(p => p.Id == order.ProductId);
            order.OrderDate = DateTime.Now;
            order.TotalAmount = variant.Price * order.Quantity;
            order.OrderStateId = 1;
            variant.Stock -= order.Quantity;
            product.TotalStock -= order.Quantity;
            _context.Order.Add(order);
            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + order.Id), order);
        }

        [HttpDelete]
        public IHttpActionResult DeleteOrder(int id)
        {
            var orderIDb = _context.Order.SingleOrDefault(m => m.Id == id);
            if (orderIDb == null)
            {
                return NotFound();
            }
            var variant = _context.Variant.SingleOrDefault(m => m.Id == orderIDb.VariantId);
            var product = _context.Product.SingleOrDefault(m => m.Id == orderIDb.ProductId);
            if(orderIDb.OrderStateId != 5)
            {
                variant.Stock += orderIDb.Quantity;
                product.TotalStock += orderIDb.Quantity;
            }
            _context.Order.Remove(orderIDb);
            _context.SaveChanges();
            return Ok();
        }
    }
}
