using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
    public class MoviesController : Controller
    {
        private ApplicationDbContext dbContext = new ApplicationDbContext();
        private ApplicationUserManager userManager = null;
        private IList<string> roles = new List<string>();
        private ApplicationUser user = null;

        // GET: Movies
        public ActionResult Index()
        {
            var movies = dbContext.Movies.ToList();

            userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            user = userManager.FindByEmail(User.Identity.Name);
            if (user != null)
                roles = userManager.GetRoles(user.Id);
            var admin = roles.Contains("admin");
            ViewBag.Admin = admin;

            return View(movies);

        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieModels movieModels = dbContext.Movies.Find(id);
            if (movieModels == null)
            {
                return HttpNotFound();
            }
            return View(movieModels);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            MovieViewModel movieView = new MovieViewModel
            {
                Movie = new MovieModels(),
                Genres = dbContext.MovieGenres.ToList()
            };

            return View(movieView);
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind(Include = "Id,Name,Director,ReleaseDate,RunningTime")]
        public ActionResult Create(MovieViewModel movieView)
        {
            if (ModelState.IsValid)
            {
                movieView.Movie.MovieGenreModels = dbContext.MovieGenres.Find(movieView.Movie.MovieGenreModels.Id);

                dbContext.Movies.Add(movieView.Movie);
                dbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MovieModels movieModels = dbContext.Movies.Include(g => g.MovieGenreModels).Where(i => i.Id == id).Single();

            if (movieModels == null)
            {
                return HttpNotFound();
            }

            MovieViewModel movieView = new MovieViewModel
            {
                Movie = movieModels,
                Genres = dbContext.MovieGenres.ToList()
            };

            return View(movieView);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind(Include = "Id,Name,Director,ReleaseDate,RunningTime")]
        public ActionResult Edit(MovieViewModel movieView)
        {
            if (ModelState.IsValid)
            {                
                var movieInDb = dbContext.Movies.Single(c => c.Id == movieView.Movie.Id);
                var genre = dbContext.MovieGenres.Find(movieView.Movie.MovieGenreModels.Id);
                movieInDb.MovieGenreModels = genre;
                dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
            //return View(movieView);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieModels movieModels = dbContext.Movies.Find(id);
            if (movieModels == null)
            {
                return HttpNotFound();
            }
            return View(movieModels);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MovieModels movieModels = dbContext.Movies.Find(id);
            dbContext.Movies.Remove(movieModels);
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
