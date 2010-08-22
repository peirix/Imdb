using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Imdb.Models;
using Imdb.ViewModels;
using System.Web.Security;

namespace Imdb.Controllers
{
    public class UserController : Controller
    {
        IMovieRepository _movieRepository;
        IBadgeRepository _badgeRepository;
        ISeenRepository _seenRepository;

        public UserController()
        {
            _movieRepository = new MovieRepository();
            _badgeRepository = new BadgeRepository();
            _seenRepository = new SeenRepository();
        }

        public UserController(IMovieRepository movieRepository, ISeenRepository seenRepository, IBadgeRepository badgeRepository)
        {
            _movieRepository = movieRepository;
            _seenRepository = seenRepository;
            _badgeRepository = badgeRepository;
        }

        //
        // GET: /User/
        // GET: /User/username

        [Authorize]
        public ActionResult Index(string user)
        {
            string username = user ?? User.Identity.Name;
            List<Movie> movies = _movieRepository.GetMoviesByUser(username).ToList();
            List<Badge> badges = _badgeRepository.GetUserBadges(username).ToList();
            
            //TODO: Look into MovieList builder
            /*
            var viewmodel = new UserIndexViewModel
            {
                MovieList = new MovieList
                                {
                                    Movies = movies,
                                    SeenMovies = movies.Select(m => m.ID).ToList()
                                },
                Badges = badges,
                Username = username
            };*/

            return View();//viewmodel);
        }

        [Authorize]
        public ActionResult Compare(string id)
        {
            List<Movie> myMovies = _movieRepository.GetMoviesByUser(User.Identity.Name).ToList();
            List<Movie> otherSeen = _movieRepository.GetMoviesByUser(id).ToList();

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
