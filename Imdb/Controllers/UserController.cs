using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Imdb.Models;
using System.Web.Security;

namespace Imdb.Controllers
{
    public class UserController : Controller
    {
        MovieRepository movieRepository = new MovieRepository();
        BadgeRepository badgeRepository = new BadgeRepository();
        SeenRepository seenRepository = new SeenRepository();

        //
        // GET: /User/
        // GET: /User/username

        [Authorize]
        public ActionResult Index(string user)
        {
            string username = user ?? User.Identity.Name;
            List<Movie> movies = movieRepository.GetMoviesByUser(username).ToList();
            List<Badge> badges = badgeRepository.GetUserBadges(username).ToList();

            ViewData["username"] = username;
            ViewData["badges"] = badges;

            ViewData["seenMovies"] = seenRepository.GetSeenMoviesByUser(User.Identity.Name).ToList();

            return View(movies);
        }

        [Authorize]
        public ActionResult Compare(string id)
        {
            List<Movie> myMovies = movieRepository.GetMoviesByUser(User.Identity.Name).ToList();
            List<Movie> otherSeen = movieRepository.GetMoviesByUser(id).ToList();

            List<Movie> bothSeen = new List<Movie>();
            List<Movie> mySeen = new List<Movie>();

            foreach (var movie in myMovies)
            {
                if (otherSeen.Contains(movie))
                {
                    bothSeen.Add(movie);
                    otherSeen.Remove(movie);
                }
                else
                {
                    mySeen.Add(movie);
                }
                
            }

            ViewData["mySeen"] = mySeen;
            ViewData["bothSeen"] = bothSeen;
            ViewData["otherSeen"] = otherSeen;

            return View(myMovies);
        }
    }
}
