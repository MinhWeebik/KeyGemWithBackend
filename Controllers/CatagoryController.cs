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
    public class CatagoryController : Controller
    {
        // GET: Catagory
        private ApplicationDbContext _context;
        public CatagoryController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Catagory
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            var ViewModels = new NewCatagoryViewModel()
            {
                Catagory = new Catagories()
            };
            return View(ViewModels);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Catagories Catagory)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new NewCatagoryViewModel { Catagory = Catagory };
                return View("New", viewModel);
            }
            if (Catagory.Id == 0)
            {
                _context.Catagories.Add(Catagory);
                _context.SaveChanges();
                TempData["CreateNewCatagorySuccess"] = "Success";
                return RedirectToAction("New");
            }
            else
            {
                var catagoryIDb = _context.Catagories.Single(c => c.Id == Catagory.Id);
                catagoryIDb.Name = Catagory.Name;
                _context.SaveChanges();
                TempData["EditNewCatagorySuccess"] = "Success";
                return RedirectToAction("Index", "Catagory");
            }
            
        }

        public ActionResult Edit(int id)
        {
            var catagory = _context.Catagories.SingleOrDefault(c => c.Id == id);
            if (catagory == null)
            {
                return HttpNotFound();
            }
            var viewModel = new NewCatagoryViewModel
            {
                Catagory = catagory
            };
            return View("New", viewModel);
        }
    }
}