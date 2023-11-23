using KeyGem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KeyGem.Controllers.Api
{
    public class PersonController : ApiController
    {
        private ApplicationDbContext _context;

        public PersonController()
        {
            _context = new ApplicationDbContext();
        }
        public IHttpActionResult GetPerson()
        {
            var personData = _context.Person.ToList();
            return Ok(personData);
        }
        public IHttpActionResult GetPersonById(int id)
        {
            var personData = _context.Person.FirstOrDefault(x => x.Id == id);
            if (personData == null)
            {
                return NotFound();
            }
            return Ok(personData);
        }
        [HttpPost]
        public IHttpActionResult CreatePerson(Person personData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _context.Person.Add(personData);
            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + personData.Id), personData);
        }
        [HttpDelete]
        public IHttpActionResult DeletePerson(int id)
        {
            var personIDb = _context.Person.SingleOrDefault(m => m.Id == id);
            var orderIDb = _context.Order.SingleOrDefault(x => x.PersonId == id);
            if(orderIDb != null)
            {
                return BadRequest();
            }
            if (personIDb == null)
            {
                return NotFound();
            }
            _context.Person.Remove(personIDb);
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
