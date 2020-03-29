using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly15.Models;

namespace Vidly15.ViewModels
{
    public class RandomMovieViewModel
    {
        public Movie Movie { get; set; }
        public List<Customer> Customers { get; set; }
    }
}