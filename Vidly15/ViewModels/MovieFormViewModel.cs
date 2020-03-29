using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly15.Models;

namespace Vidly15.ViewModels
{
    public class MovieFormViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }

        //public Movie Movie { get; set; }

        public int? Id { get; set; } //implicitly marked as required by ASP.NET

        [Required(ErrorMessage = "Please provide a Movie Name.")]
        [Display(Name = "Movie Name")]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        public DateTime? ReleaseDate { get; set; }

        //public DateTime DateAdded { get; set; }

        [Required]
        [Display(Name = "Number In Stock")]
        [Range(1, 20)]
        public byte? NumberInStock { get; set; }

        //public Genre Genre { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public byte? GenreId { get; set; }

        public string Title
        { get
            {
                if (Id != 0)
                    return "Edit Movie";

                return "New Movie";
            }
        }

        public MovieFormViewModel()
        {
            Id = 0;
        }

        public MovieFormViewModel(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            ReleaseDate = movie.ReleaseDate;
            NumberInStock = movie.NumberInStock;
            GenreId = movie.GenreId;
        }
    }
}