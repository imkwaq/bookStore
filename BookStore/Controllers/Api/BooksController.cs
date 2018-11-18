using AutoMapper;
using BookStore.Dtos;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookStore.Controllers.Api
{
    public class BooksController : ApiController
    {
        private ApplicationDbContext applicationDbContext;

        public BooksController()
        {
            applicationDbContext = new ApplicationDbContext();
        }

        //GET api/books/
        public IEnumerable<BookDto> GetBooks()
        {
            return applicationDbContext.Books.ToList().Select(Mapper.Map<BookModels, BookDto>);             
        }

        //GET api/books/1
        public BookDto GetBook(int id)
        {
            var book = applicationDbContext.Books.SingleOrDefault(b => b.Id == id);

            if (book == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<BookModels, BookDto>(book);
        }

        //POST api/books
        [HttpPost]
        public BookModels CreateBook(BookModels book)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            applicationDbContext.Books.Add(book);
            applicationDbContext.SaveChanges();

            return book;
        }

        //PUT api/books/1
        [HttpPut]
        public void UpdateBook(int id, BookModels book)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var bookInDb = applicationDbContext.Books.SingleOrDefault(b => b.Id == id);

            if (bookInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            bookInDb.Name = book.Name;
            bookInDb.Author = book.Author;
            bookInDb.ReleaseYear = book.ReleaseYear;
            bookInDb.Description = book.Description;
            bookInDb.ImageData = book.ImageData;
            bookInDb.ImageMimeType = book.ImageMimeType;

            applicationDbContext.SaveChanges();
        }

        //DELETE api/books/1
        [HttpDelete]
        public void DeleteBook(int id)
        {
            var bookInDb = applicationDbContext.Books.SingleOrDefault(b => b.Id == id);

            if (bookInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            applicationDbContext.Books.Remove(bookInDb);
            applicationDbContext.SaveChanges();
        }
    }
}
