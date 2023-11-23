using KeyGem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using Microsoft.Owin.Security.OAuth;

namespace KeyGem.Controllers.Api
{
    public class ProductController : ApiController
    {
        private ApplicationDbContext _context;

        public ProductController()
        {
            _context = new ApplicationDbContext();
        }
        public IHttpActionResult GetProductData(bool specialCall = false)
        {  
            var query = _context.Product.Include(m => m.Variants).Include(m => m.Catagory).Include(m => m.Variants);
            if(specialCall)
            {
                query = query.Where(p => p.TotalStock > 0);
            }
            var productData = query.ToList();
            return Ok(productData);
        }

        public IHttpActionResult GetProductData(int id)
        {
            var productData = _context.Product.FirstOrDefault(x => x.Id == id);
            if (productData == null)
            {
                return NotFound();
            }
            return Ok(productData);
        }
        [HttpPost]
        public IHttpActionResult CreateProduct(Product productData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _context.Product.Add(productData);
            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + productData.Id), productData);
        }

        [HttpPut]
        public IHttpActionResult UpdateProduct(int id, Product productData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var productIDb = _context.Product.SingleOrDefault(m => m.Id == id);
            if (productIDb == null)
            {
                return NotFound();
            }
            productIDb.Name = productData.Name;
            productIDb.Description = productData.Description;
            productIDb.CatagoriesId = productData.CatagoriesId;
            _context.SaveChanges();
            return Ok();

        }

        [HttpDelete]
        public IHttpActionResult DeleteProduct(int id)
        {
            var productIDb = _context.Product.SingleOrDefault(m => m.Id == id);
            if (productIDb == null)
            {
                return NotFound();
            }
            var orderIdb = _context.Order.SingleOrDefault(o => o.ProductId == id);
            if(orderIdb != null)
            {
                return BadRequest();
            }
            var variantList = _context.Variant.Where(v => v.ProductId == id);
            if(variantList != null)
            {
                foreach (var variant in variantList)
                {
                    _context.Variant.Remove(variant);
                }
            }
            _context.Product.Remove(productIDb);
            _context.SaveChanges();
            return Ok();
        }
    }
}
