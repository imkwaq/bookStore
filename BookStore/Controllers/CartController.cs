using BookStore.Models;
using BookStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cart
        public ViewResult Index(CartModels cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(CartModels cart, int id, string returnUrl)
        {
            BookModels book = db.Books
                .FirstOrDefault(b => b.Id == id);

            if (book != null)
            {
                cart.AddItem(book, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(CartModels cart, int id, string returnUrl)
        {
            BookModels book = db.Books
                .FirstOrDefault(g => g.Id == id);

            if (book != null)
            {
                cart.RemoveLine(book);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(CartModels cart)
        {
            return PartialView(cart);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}