using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Imdb.Models;

namespace Imdb.ViewModels
{
    public class UserIndexViewModel
    {
        public List<Badge> Badges { get; set; }
        public MovieList MovieList { get; set; }
        public string Username { get; set; }
    }
}