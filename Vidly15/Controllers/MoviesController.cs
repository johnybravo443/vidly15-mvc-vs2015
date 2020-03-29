using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly15.Models;
using Vidly15.ViewModels;
using System.Data.Entity.Validation;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [Authorize(Roles = RoleName.CanManageMovies)]  //This will override the global Authorize attribute. 
        public ActionResult New()
        {
            var genres = _context.Genres;
            var ViewModel = new MovieFormViewModel
            {
                Genres = genres
            };

            return View("MovieForm",ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie) //or MovieFormViewModel viewModel will also work
        {
            if (!ModelState.IsValid)
            {
                var ViewModel = new MovieFormViewModel(movie)
                {
                    Genres = _context.Genres.ToList()
                };

                return View("MovieForm", ViewModel);
            }

            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                movie.NumberAvailable = movie.NumberInStock;
                _context.Movies.Add(movie); // changes in memory and not yet commited to DB
            }
            else
            {
                var movieInDb = _context.Movies.Single(c => c.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.NumberAvailable = movie.NumberInStock; //since we can increase or decrease the NumberInStock
            }

            _context.SaveChanges(); //changes commited to DB in one transaction.

            return RedirectToAction("Index", "Movies");
        }

        // GET: Customers
        public ActionResult Index()
        {
            //var movies = _context.Movies.Include(c => c.Genre).ToList();
            //return View(movies);
            if (User.IsInRole(RoleName.CanManageMovies))
                return View("List");

            return View("ReadOnlyList");
        }

        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(c => c.Genre).SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return HttpNotFound();

            var ViewModel = new MovieFormViewModel(movie)
            {
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", ViewModel);
        }

        //private IEnumerable<Movie> GetMovies()
        //{
        //    return new List<Movie>
        //    {
        //        new Movie { Id = 1, Name = "Shrek" },
        //        new Movie { Id = 2, Name = "Wall-e" }
        //    };

        //}

        // GET: Movies/Random
        //public ActionResult Random()
        //{
        //    var movie = new Movie() { Name = "Shrek!" };

        //    var customers = new List<Customer>
        //    {
        //        new Customer { Name = "Customer 1"},
        //        new Customer { Name = "Customer 2"}
        //    };

        //    var viewModel = new RandomMovieViewModel
        //    {
        //        Movie = movie,
        //        Customers = customers
        //    };

        //    return View(viewModel);
        //    //return Content("Hello World!");
        //    //return HttpNotFound();
        //    //return RedirectToAction("Index", "Home", new { page = 1, sortBy = "name" });
        //}

        //public ActionResult Edit(int id)
        //{
        //    return Content("id = " + id);
        //}

        ////Movies
        //public ActionResult Index(int? pageIndex, string sortBy)
        //{
        //    if (!pageIndex.HasValue)
        //        pageIndex = 1;

        //    if (string.IsNullOrWhiteSpace(sortBy))
        //        sortBy = "Name";

        //    return Content(String.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));
        //}

        //[Route("movies/released/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(1, 12)}")]
        //public ActionResult ByReleaseDate(int year, int month)
        //{
        //    return Content(year + "/" + month);
        //}
    }
}