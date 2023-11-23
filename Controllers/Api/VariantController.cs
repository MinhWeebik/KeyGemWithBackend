using KeyGem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KeyGem.Controllers.Api
{
    public class VariantController : ApiController
    {
        private ApplicationDbContext _context;

        public VariantController()
        {
            _context = new ApplicationDbContext();
        }
        public IHttpActionResult GetVariantWithId(int id, bool specialCall = false)
        {
            var query = _context.Variant.Where(x => x.ProductId == id);
            if (specialCall)
            {
                query = query.Where(q => q.Stock > 0);
            }
            var variantData = query.ToList();
            return Ok(variantData);
        }

        [HttpDelete]
        public IHttpActionResult DeleteVariant(int id)
        {
            var variantIDb = _context.Variant.SingleOrDefault(m => m.Id == id);
            var orderIDb = _context.Order.SingleOrDefault(o => o.VariantId == id);
            if(orderIDb != null)
            {
                return BadRequest();
            }
            if (variantIDb == null)
            {
                return NotFound();
            }
            _context.Product.Single(p => p.Id == variantIDb.ProductId).TotalStock -= variantIDb.Stock;
            _context.Variant.Remove(variantIDb);
            _context.SaveChanges();
            return Ok();
        }
    }
}
