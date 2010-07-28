using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Imdb.Models;
using Imdb.Helpers;

namespace Imdb.ViewModels
{
    public class MovieListViewModel
    {
        public PaginatedList<Movie> PaginatedMovies { get; set; }
        public List<int> PageSizeOptions { get; set; }
        public List<int> SeenMovies { get; set; }

        public MovieListViewModel()
        {
            PageSizeOptions = new List<int> { 10, 20, 50, 100, 250 };
        }
        
    }
}