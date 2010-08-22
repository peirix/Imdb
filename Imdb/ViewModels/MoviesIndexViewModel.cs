using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Imdb.Models;
using Imdb.Helpers;

namespace Imdb.ViewModels
{
    public class MoviesIndexViewModel
    {
        public MovieList MovieList { get; set; }
        public List<int> PageSizeOptions { get; set; }
        public DateTime LastUpdated { get; set; }

        public MoviesIndexViewModel()
        {
            PageSizeOptions = new List<int> { 10, 20, 50, 100, 250 };
        }
        
    }
}