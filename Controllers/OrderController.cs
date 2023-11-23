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
    public class OrderController : Controller
    {
        private ApplicationDbContext _context;
        public OrderController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New() {
            var viewModel = new NewOrderViewModel()
            {
                order = new Order(),
                OrderStates = _context.OrderState.ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Save(Order order)
        {
            int variantId;
            bool isSuccess = Int32.TryParse(Request.Form["VariantId"], out variantId);
            if (!ModelState.IsValid || !isSuccess)
            {
                var variant2 = (variantId != 0) ? _context.Variant.Single(x => x.Id == variantId) : null;
                var product2 = (order.ProductId != 0) ? _context.Product.Single(p => p.Id == order.ProductId) : null;
                var person = (order.PersonId != 0) ? _context.Person.Single(p => p.Id == order.PersonId) : null;
                order.Product = product2;
                order.Variant = variant2;
                order.Person = person;
                order.VariantId = variantId;
                if(!isSuccess)
                {
                    TempData["NoVariant"] = "The varriant field is required.";
                }
                var viewModel = new NewOrderViewModel()
                {
                    order = order,
                    OrderStates = _context.OrderState.ToList()
                };
                return View("New", viewModel);
            }
            var variant = _context.Variant.Single(x => x.Id == variantId);
            var product = _context.Product.Single(p => p.Id == order.ProductId);
            
            if (order.Id == 0)
            {
                if (variant.Stock < order.Quantity)
                {
                    var variant2 = (order.VariantId != 0) ? _context.Variant.Single(x => x.Id == variantId) : null;
                    var product2 = (order.ProductId != 0) ? _context.Product.Single(p => p.Id == order.ProductId) : null;
                    var person = (order.PersonId != 0) ? _context.Person.Single(p => p.Id == order.PersonId) : null;
                    order.Product = product2;
                    order.Variant = variant2;
                    order.Person = person;
                    order.VariantId = variantId;

                    ModelState.AddModelError("OutOfStock", "The quantity can't be greater than " + variant.Stock);
                    var viewModel = new NewOrderViewModel()
                    {
                        order = order,
                        OrderStates = _context.OrderState.ToList()
                    };
                    return View("New", viewModel);
                }
                order.OrderDate = DateTime.Now;
                order.TotalAmount = variant.Price * order.Quantity;
                order.OrderStateId = 1;
                order.VariantId = variantId;
                variant.Stock -= order.Quantity;
                product.TotalStock -= order.Quantity;
                _context.Order.Add(order);
                _context.SaveChanges();
                TempData["CreateNewOrderSuccess"] = "Success";
                var viewModel2 = new NewOrderViewModel()
                {
                    order = new Order(),
                    OrderStates = _context.OrderState.ToList()
                };
                return View("New", viewModel2);
            }
            else
            {
                
                var orderIDb = _context.Order.Single(c => c.Id == order.Id);
                if ((variant.Stock < order.Quantity) && orderIDb.Quantity != order.Quantity)
                {
                    var variant2 = (order.VariantId != 0) ? _context.Variant.Single(x => x.Id == variantId) : null;
                    var product2 = (order.ProductId != 0) ? _context.Product.Single(p => p.Id == order.ProductId) : null;
                    var person = (order.PersonId != 0) ? _context.Person.Single(p => p.Id == order.PersonId) : null;
                    order.Product = product2;
                    order.Variant = variant2;
                    order.Person = person;
                    order.VariantId = variantId;
                    ModelState.AddModelError("OutOfStock", "The quantity can't be greater than " + variant.Stock);
                    var viewModel = new NewOrderViewModel()
                    {
                        order = order,
                        OrderStates = _context.OrderState.ToList()
                    };
                    return View("New", viewModel);
                }

                var oldVariant = _context.Variant.Single(v => v.Id == orderIDb.VariantId);
                var oldOrderQuantity = orderIDb.Quantity;
                var oldProduct = _context.Product.Single(x => x.Id == orderIDb.ProductId);
                if (oldVariant == variant)
                {
                    var difference = oldOrderQuantity - order.Quantity;
                    variant.Stock += difference;
                    product.TotalStock += difference;
                }
                else
                {
                    oldVariant.Stock += oldOrderQuantity;
                    oldProduct.TotalStock += oldOrderQuantity;
                    variant.Stock -= order.Quantity;
                    product.TotalStock -= order.Quantity;
                }
                orderIDb.PersonId = order.PersonId;
                orderIDb.ProductId = order.ProductId;
                orderIDb.VariantId = variantId;
                orderIDb.TotalAmount = variant.Price * order.Quantity;
                orderIDb.Quantity = order.Quantity;
                orderIDb.OrderStateId = order.OrderStateId;
                _context.SaveChanges();
                TempData["EditNewOrderSuccess"] = "Success";
                return RedirectToAction("Index", "Order");
            }
            
            
        }

        public ActionResult Edit(int id)
        {
            var order = _context.Order.SingleOrDefault(c => c.Id == id);
            var product = _context.Product.SingleOrDefault(p => p.Id == order.ProductId);
            var person = _context.Person.SingleOrDefault(o => o.Id == order.PersonId);
            var variation = _context.Variant.SingleOrDefault(o => o.Id == order.VariantId);
            order.Person = person;
            order.Product = product;
            order.Variant = variation;
            if (order == null)
            {
                return HttpNotFound();
            }
            TempData["FirstName"] = person.FirstName;
            var viewModel = new NewOrderViewModel()
            {
                order = order,
                OrderStates = _context.OrderState.ToList()
            };
            return View("New", viewModel);
        }

        public ActionResult Detail(int id)
        {
            var order = _context.Order.SingleOrDefault(c => c.Id == id);
            var product = _context.Product.SingleOrDefault(p => p.Id == order.ProductId);
            var person = _context.Person.SingleOrDefault(o => o.Id == order.PersonId);
            var variation = _context.Variant.SingleOrDefault(o => o.Id == order.VariantId);
            var orderState = _context.OrderState.SingleOrDefault(o => o.Id == order.OrderStateId);
            order.Person = person;
            order.Product = product;
            order.Variant = variation;
            order.OrderState = orderState;
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }
    }
}