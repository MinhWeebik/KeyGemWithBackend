using KeyGem.Models;
using KeyGem.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.WebSockets;

namespace KeyGem.Controllers
{
    [AllowAnonymous]
    public class MainPageController : Controller
    {
        private ApplicationDbContext _context;
        public MainPageController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: MainPage
        public ActionResult Index()
        {
            var product = _context.Product.Include(p => p.Variants).Where(p => p.TotalStock > 0).ToList();
            var shuffledItems = product.OrderBy(x => Guid.NewGuid()).ToList();
            var newItemFirstPage = shuffledItems.Take(4).ToList();
            shuffledItems.RemoveAll(item => newItemFirstPage.Contains(item));
            var newItemSecondPage = shuffledItems.Take(4).ToList();
            shuffledItems.RemoveAll(item => newItemSecondPage.Contains(item));
            var bestSellerFirstPage = shuffledItems.Take(4).ToList();
            shuffledItems.RemoveAll(item => bestSellerFirstPage.Contains(item));
            var bestSellerSecondPage = shuffledItems.Take(4).ToList();
            var viewModel = new MainPageItems()
            {
                NewItemFirstPage = newItemFirstPage,
                NewItemSecondPage = newItemSecondPage,
                BestSellerFirstPage = bestSellerFirstPage,
                BestSellerSecondPage= bestSellerSecondPage
            };

            return View(viewModel);
        }

        public ActionResult AllProduct(int page = 1)
        {
            var products = _context.Product.Include(p => p.Variants).Include(p => p.Catagory).ToList();

            int totalProducts = products.Count();
            int totalPages = (int)Math.Ceiling((double)totalProducts / 16);
            int removeRange = (16 * (page - 1));
            if (page == 1)
            {
                if(totalProducts >= 16)
                {
                    products = products.Take(16).ToList();
                }
            }
            else
            {
                products.RemoveRange(0, removeRange);
            }
            var viewModel = new AllProductViewModel()
            {
                Products = products,
                CurrentPage = page,
                PageAmount = totalPages,
            };
            return View(viewModel);
        }

        public ActionResult ProductDetail(int id)
        {
            var products = _context.Product.Include(p => p.Variants).ToList();
            var currentProduct = products.SingleOrDefault(p => p.Id == id);
            
            var shuffledItems = products.OrderBy(x => Guid.NewGuid()).ToList();
            var recommendItem = shuffledItems.Take(4).ToList();
            var viewModel = new ProductDetailViewModel()
            {
                Product = currentProduct,
                RecommendProducts = recommendItem
            };

            if (currentProduct == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            var user = new ApplicationUser();
            var person = new Person();
            if (User.Identity.IsAuthenticated)
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;

                string userId = claimsIdentity?.FindFirstValue(ClaimTypes.NameIdentifier);

                user = _context.Users.Single(u => u.Id == userId);
                person = _context.Person.SingleOrDefault(p => p.Id == user.PersonId);
                if (person == null)
                {
                    person = new Person();
                }
            }
            MainPageLoginViewModel viewModel = new MainPageLoginViewModel()
            {
                Email = null,
                Password = null,
                CurrentEmail = user.Email,
                Person = person
            };
            return View(viewModel);
        }

        public ActionResult Register()
        {
            RegisterViewModel viewModel = new RegisterViewModel()
            {
                RoleName = "User"
            };
            return View(viewModel);
        }

        public ActionResult CheckOut()
        {
            string userId = User.Identity.GetUserId();
            var currentUser = _context.Users.Include(u => u.Person).SingleOrDefault(u => u.Id == userId);
            if(currentUser == null) { currentUser = new ApplicationUser(); }
            var viewModel = new CheckoutViewModel()
            {
                User = currentUser,
                Person = currentUser.PersonId == 0 || currentUser.PersonId == null ? TempData["Person"] !=null ? TempData["Person"] as Person : new Person() : currentUser.Person ,
            };
            return View(viewModel);
        }

        public ActionResult OrderComplete()
        {
            return View();
        }
    }
}