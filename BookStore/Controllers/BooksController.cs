using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
using BookStore.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace BookStore.Controllers
{
    public class BooksController : Controller
    {
        private ApplicationDbContext dbContext = new ApplicationDbContext();
        private IList<string> roles = new List<string>();
        private ApplicationUserManager userManager = null;
        private ApplicationUser user = null;

        // GET: Books
        public ActionResult Index()
        {            
            var books = dbContext.Books.Include(book => book.MovieModels).ToList();

            userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            user = userManager.FindByEmail(User.Identity.Name);
            if (user != null)
                roles = userManager.GetRoles(user.Id);
            var admin = roles.Contains("admin");
            ViewBag.Admin = admin;

            return View(books);
        }

        public FileContentResult GetImage(int bookId)
        {
            BookModels book = dbContext.Books
                .FirstOrDefault(g => g.Id == bookId);

            if (book != null)
            {
                return File(book.ImageData, book.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookModels bookModels = dbContext.Books.Find(id);
            if (bookModels == null)
            {
                return HttpNotFound();
            }

            userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            user = userManager.FindByEmail(User.Identity.Name);
            if (user != null)
                roles = userManager.GetRoles(user.Id);
            var admin = roles.Contains("admin");
            ViewBag.Admin = admin;

            return View(bookModels);
        }

        // GET: Books/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            var genres = dbContext.BookGenres.ToList();
            var movies = dbContext.Movies.ToList();
            var viewModel = new BookViewModel()
            {
                Book = new BookModels(),
                Genres = genres,
                RelatedMovie = movies
            };
            return View(viewModel);
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        //[Bind(Include = "Id,Name,Author,ReleaseYear,Description,Price")]
        public ActionResult Create(BookViewModel bookModels, HttpPostedFileBase uploadImage) /*, HttpPostedFileBase uploadImage*/
        {
            //&& uploadImage != null
            if (ModelState.IsValid)
            {
                if (uploadImage != null)
                {
                    byte[] imageData = null;
                    // считываем переданный файл в массив байтов
                    using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                    }
                    // установка массива байтов
                    bookModels.Book.ImageMimeType = uploadImage.ContentType;
                    bookModels.Book.ImageData = imageData;
                }

                if (bookModels.Book.MovieModels.Id == null)
                {
                    bookModels.Book.MovieModels.Id = 0;
                }

                bookModels.Book.BookGenreModels = dbContext.BookGenres.Find(bookModels.Book.BookGenreModels.Id);
                bookModels.Book.MovieModels = dbContext.Movies.Find(bookModels.Book.MovieModels.Id);
                dbContext.Books.Add(bookModels.Book);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bookModels);
        }

        // GET: Books/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookModels bookModels = dbContext.Books.Find(id);
            if (bookModels == null)
            {
                return HttpNotFound();
            }

            userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            user = userManager.FindByEmail(User.Identity.Name);
            if (user != null)
                roles = userManager.GetRoles(user.Id);

            ViewBag.Roles = roles;

            return View(bookModels);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit([Bind(Include = "Id,Name,Author,ReleaseYear,Description,Price")] BookModels bookModels, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid && uploadImage != null)
            {
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }
                // установка массива байтов
                bookModels.ImageMimeType = uploadImage.ContentType;
                bookModels.ImageData = imageData;
                

                dbContext.Entry(bookModels).State = EntityState.Modified;
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bookModels);
        }

        // GET: Books/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookModels bookModels = dbContext.Books.Find(id);
            if (bookModels == null)
            {
                return HttpNotFound();
            }
            return View(bookModels);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            BookModels bookModels = dbContext.Books.Find(id);
            dbContext.Books.Remove(bookModels);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
