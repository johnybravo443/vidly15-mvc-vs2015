using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly15.Dto
{
    public class MovieDto
    {
        public int Id { get; set; } //implicitly marked as required by ASP.NET

        [Required(ErrorMessage = "Please provide a Movie Name.")]
        [Display(Name = "Movie Name")]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        public DateTime DateAdded { get; set; }

        [Required]
        [Display(Name = "Number In Stock")]
        [Range(1, 20)]
        public byte NumberInStock { get; set; }

        public GenreDto Genre { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public byte GenreId { get; set; }

        public byte NumberAvailable { get; set; }
    }
}