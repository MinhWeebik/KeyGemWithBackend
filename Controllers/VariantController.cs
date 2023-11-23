using KeyGem.Models;
using KeyGem.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KeyGem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VariantController : Controller
    {
        // GET: Variant
        private ApplicationDbContext _context;
        public VariantController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Catagory
        public ActionResult New(int id)
        {
            var context = _context.Product.SingleOrDefault(x => x.Id == id);
            if(context == null)
            {
                return HttpNotFound();
            }
            var ViewModels = new NewVariantViewModel()
            {
                ProductId = id
            };
            return View(ViewModels);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Variant Variant)
        {
            var viewModel = new NewVariantViewModel { Variant = Variant, ProductId = Variant.ProductId };
            if (!ModelState.IsValid)
            {
                return View("New", viewModel);
            }
            var imageFile = Request.Files["thumbnail"];
            if(imageFile.ContentLength > 0)
            {
                string extension = ".png";
                string fileName = Path.ChangeExtension(Path.GetRandomFileName(), extension);
                string folder = Server.MapPath("~/Content/Images/" + fileName);
                imageFile.SaveAs(folder);
                Variant.ImageLink = "/Content/Images/" + fileName;
            }
            else if(Variant.ImageLink == null)
            {
                ModelState.AddModelError("", "Image is required");
                return View("New", viewModel);
            }

            if (Variant.Id == 0)
            {
                _context.Variant.Add(Variant);
                _context.SaveChanges();
                var variantList = _context.Variant.Where(p => p.ProductId == Variant.ProductId).ToList();
                var product = _context.Product.Single(p => p.Id == Variant.ProductId);
                product.TotalStock = 0;
                foreach (var v in variantList)
                {
                    product.TotalStock += v.Stock;
                }
                _context.SaveChanges();
                TempData["CreateVariantSuccess"] = "Success";
                return RedirectToAction("New", "Variant", new { id = Variant.ProductId});
            }
            else
            {
                var VariantIDb = _context.Variant.Single(c => c.Id == Variant.Id);
                VariantIDb.Name = Variant.Name;
                VariantIDb.Stock = Variant.Stock;
                VariantIDb.Price = Variant.Price;
                VariantIDb.ImageLink = Variant.ImageLink;
                var variantList = _context.Variant.Where(p => p.ProductId == Variant.ProductId).ToList();
                var product = _context.Product.Single(p => p.Id == Variant.ProductId);
                product.TotalStock = 0;
                foreach (var v in variantList)
                {
                    product.TotalStock += v.Stock;
                }
                _context.SaveChanges();
                TempData["EditVariantSuccess"] = "Success";
                return RedirectToAction("Index", "Variant", new { id = Variant.ProductId });
            }
        }


        public ActionResult Index(int id)
        {
            return View(id);
        }

        public ActionResult Edit(int id)
        {
            var variant = _context.Variant.SingleOrDefault(c => c.Id == id);
            if (variant == null)
            {
                return HttpNotFound();
            }
            var viewModel = new NewVariantViewModel
            {
                Variant = variant,
                ProductId = variant.ProductId
            };
            return View("New", viewModel);
        }
    }
}