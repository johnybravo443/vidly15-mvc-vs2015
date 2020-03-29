using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly15.Dto;
using Vidly15.Models;

namespace Vidly15.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }

        //POST /api/rentals
        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRental)
        {
            // Defensive approach
            //if (newRental.MovieIds.Count == 0)
            //    return BadRequest("No Movie Ids provided.");

            var customer = _context.Customers.Single(c => c.Id == newRental.CustomerId);

            // Defensive approach
            //if (customer == null)
            //    return BadRequest("CustomerId is not valid.");

            var movies = _context.Movies.Where(m => newRental.MovieIds.Contains(m.Id)).ToList();

            // Defensive approach
            //if (movies.Count != newRental.MovieIds.Count)
            //    return BadRequest("One or more movie ids are invalid.");

            foreach(var movie in movies)
            {
                // could be used for both Defensive and optimistic approach since a hacker can make unnecessary calls
                if (movie.NumberAvailable == 0)
                    return BadRequest("movie is not available.");
                movie.NumberAvailable--;

                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };

                _context.Rentals.Add(rental);
            }

            _context.SaveChanges();

            return Ok("Rental Created");
        }
    }
}
