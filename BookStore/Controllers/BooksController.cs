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

namespace BookStore.Controllers
{
    public class BooksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Books
        public ActionResult Index()
        {
            var books = db.Books.Include(book => book.MovieModels).ToList();
            return View(books);
        }

        public FileContentResult GetImage(int bookId)
        {
            BookModels book = db.Books
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
            BookModels bookModels = db.Books.Find(id);
            if (bookModels == null)
            {
                return HttpNotFound();
            }
            return View(bookModels);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            var genres = db.BookGenres.ToList();
            var movies = db.Movies.ToList();
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

                bookModels.Book.BookGenreModels = db.BookGenres.Find(bookModels.Book.BookGenreModels.Id);
                bookModels.Book.MovieModels = db.Movies.Find(bookModels.Book.MovieModels.Id);
                db.Books.Add(bookModels.Book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bookModels);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookModels bookModels = db.Books.Find(id);
            if (bookModels == null)
            {
                return HttpNotFound();
            }
            return View(bookModels);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                

                db.Entry(bookModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bookModels);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookModels bookModels = db.Books.Find(id);
            if (bookModels == null)
            {
                return HttpNotFound();
            }
            return View(bookModels);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BookModels bookModels = db.Books.Find(id);
            db.Books.Remove(bookModels);
            db.SaveChanges();
            return RedirectToAction("Index");
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
