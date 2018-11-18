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
        public IHttpActionResult GetBook(int id)
        {
            var book = applicationDbContext.Books.SingleOrDefault(b => b.Id == id);

            if (book == null)
                return NotFound();

            return Ok(Mapper.Map<BookModels, BookDto>(book));
        }

        //POST api/books
        [HttpPost]
        public IHttpActionResult CreateBook(BookDto bookDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var book = Mapper.Map<BookDto, BookModels>(bookDto);
                        
            book.ImageData = null;
            book.ImageMimeType = null;

            applicationDbContext.Books.Add(book);
            applicationDbContext.SaveChanges();

            bookDto.Id = book.Id;
            
            return Created(new Uri(Request.RequestUri + "/" + book.Id), bookDto);
        }

        //PUT api/books/1
        [HttpPut]
        public IHttpActionResult UpdateBook(int id, BookDto bookDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var bookInDb = applicationDbContext.Books.SingleOrDefault(b => b.Id == id);

            if (bookInDb == null)
                return NotFound();

            Mapper.Map(bookDto, bookInDb);

            applicationDbContext.SaveChanges();

            return Ok();
        }

        //DELETE api/books/1
        [HttpDelete]
        public IHttpActionResult DeleteBook(int id)
        {
            var bookInDb = applicationDbContext.Books.SingleOrDefault(b => b.Id == id);

            if (bookInDb == null)
                return NotFound();

            applicationDbContext.Books.Remove(bookInDb);
            applicationDbContext.SaveChanges();

            return Ok();
        }
    }
}
