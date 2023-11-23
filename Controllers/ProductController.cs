using KeyGem.Models;
using KeyGem.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KeyGem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private ApplicationDbContext _context;
        public ProductController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult New()
        {
            var ViewModels = new NewProductViewModel()
            {
                Catagory = _context.Catagories.ToList(),
                Product = new Product()
            };
            return View(ViewModels);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Product Product)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new NewProductViewModel { Product = Product, Catagory = _context.Catagories.ToList() };
                return View("New", viewModel);
            }
            if (Product.Id == 0)
            {
                _context.Product.Add(Product);
                _context.SaveChanges();
                TempData["Success"] = "Them thanh cong";
                return RedirectToAction("New", "Variant", new { Product.Id });
            }
            else
            {
                var productIDb = _context.Product.Single(c => c.Id == Product.Id);
                productIDb.Name = Product.Name;
                productIDb.CatagoriesId = Product.CatagoriesId;
                productIDb.Description = Product.Description;
                _context.SaveChanges();
                TempData["Success"] = "Sua thanh cong";
                return RedirectToAction("Index", "Product");
            }
            
        }

        public ActionResult Edit(int id)
        {
            var product = _context.Product.SingleOrDefault(c => c.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            var viewModel = new NewProductViewModel
            {
                Catagory = _context.Catagories.ToList(),
                Product = product,
            };
            return View("New", viewModel);
        }

        public ActionResult Detail(int id)
        {
            var product = _context.Product.Include(p => p.Variants).SingleOrDefault(c => c.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            var viewModel = new NewProductViewModel
            {
                Catagory = _context.Catagories.ToList(),
                Product = product,
            };
            return View(viewModel);
        }
    }
}