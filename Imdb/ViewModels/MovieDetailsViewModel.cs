using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Imdb.Models;

namespace Imdb.ViewModels
{
    public class MovieDetailsViewModel
    {
        public Movie Movie { get; set; }
        public List<string> SeenBy { get; set; }
        public List<int> RankLog { get; set; }
    }
}