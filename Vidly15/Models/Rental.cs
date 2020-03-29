using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly15.Models
{
    public class Rental
    {
        public int Id { get; set; }

        [Required]
        public Customer Customer { get; set; }
        //public int CustomrId { get; set; }

        [Required]
        public Movie Movie { get; set; }
        //public int MovieId { get; set; }

        public DateTime? DateRented { get; set; }

        public DateTime? DateReturned { get; set; }
    }
}