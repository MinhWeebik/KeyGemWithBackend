using KeyGem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KeyGem.Controllers.Api
{
    public class CatagoryController : ApiController
    {
        private ApplicationDbContext _context;

        public CatagoryController()
        {
            _context = new ApplicationDbContext();
        }
        public IHttpActionResult GetCatagory()
        {
            var catagoryData = _context.Catagories.ToList();
            return Ok(catagoryData);
        }
        [HttpDelete]
        public IHttpActionResult DeleteCatagory(int id)
        {
            var catagoryIDb = _context.Catagories.SingleOrDefault(m => m.Id == id);
            if (catagoryIDb == null)
            {
                return NotFound();
            }
            _context.Catagories.Remove(catagoryIDb);
            try
            {
                _context.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
