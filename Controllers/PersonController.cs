using KeyGem.Models;
using KeyGem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KeyGem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PersonController : Controller
    {
        private ApplicationDbContext _context;
        public PersonController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Person
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            var ViewModels = new NewPersonViewModel()
            {
                Person = new Person()
            };
            return View(ViewModels);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Person Person)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new NewPersonViewModel { Person = Person };
                return View("New", viewModel);
            }
            if (Person.Id == 0)
            {
                _context.Person.Add(Person);
                _context.SaveChanges();
                TempData["CreateNewPersonSuccess"] = "Success";
                return RedirectToAction("New");
            }
            else
            {
                var personIDb = _context.Person.Single(c => c.Id == Person.Id);
                personIDb.FirstName = Person.FirstName;
                personIDb.LastName = Person.LastName;
                personIDb.Phone = Person.Phone;
                personIDb.Address = Person.Address;
                _context.SaveChanges();
                TempData["EditNewPersonSuccess"] = "Success";
                return RedirectToAction("Index", "Person");
            }
            
        }

        public ActionResult Edit(int id)
        {
            var person = _context.Person.SingleOrDefault(c => c.Id == id);
            if (person == null)
            {
                return HttpNotFound();
            }
            var viewModel = new NewPersonViewModel
            {
                Person = person
            };
            return View("New", viewModel);
        }
    }
}