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
    public class MoviesController : ApiController
    {
        private ApplicationDbContext applicationDbContext;

        public MoviesController()
        {
            applicationDbContext = new ApplicationDbContext();
        }

        //GET api/movies/
        public IEnumerable<MovieDto> GetMovies()
        {
            return applicationDbContext.Movies.ToList().Select(Mapper.Map<MovieModels, MovieDto>);
        }

        //GET api/movies/1
        public IHttpActionResult GetMovie(int id)
        {
            var movie = applicationDbContext.Movies.SingleOrDefault(b => b.Id == id);

            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<MovieModels, MovieDto>(movie));
        }

        //POST api/movies
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = Mapper.Map<MovieDto, MovieModels>(movieDto);

            applicationDbContext.Movies.Add(movie);
            applicationDbContext.SaveChanges();

            movieDto.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        //PUT api/movies/1
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movieInDb = applicationDbContext.Books.SingleOrDefault(b => b.Id == id);

            if (movieInDb == null)
                return NotFound();

            Mapper.Map(movieDto, movieInDb);

            applicationDbContext.SaveChanges();

            return Ok();
        }

        //DELETE api/movies/1
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movieInDb = applicationDbContext.Movies.SingleOrDefault(b => b.Id == id);

            if (movieInDb == null)
                return NotFound();

            applicationDbContext.Movies.Remove(movieInDb);
            applicationDbContext.SaveChanges();

            return Ok();
        }

    }
}
