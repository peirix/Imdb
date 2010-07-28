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
    }
}