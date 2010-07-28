using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Imdb.Models;
using Imdb.Helpers;
using HtmlAgilityPack;
using System.Net;
using System.IO;
using Imdb.ViewModels;

namespace Imdb.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ISeenRepository _seenRepository;
        
        public MoviesController()
        {
            _movieRepository = new MovieRepository();
            _seenRepository = new SeenRepository();
        }

        public MoviesController(IMovieRepository movieRepository, ISeenRepository seenRepository)
        {
            _seenRepository = seenRepository;
            _movieRepository = movieRepository;
        }

        //
        // GET: /Movie/

        public ActionResult Index(int? page, int? pageSize)
        {
            var movies = _movieRepository.AllMovies();
            var paginatedMovies = new PaginatedList<Movie>(movies, page ?? 0, pageSize ?? 20);

            var viewmodel = new MovieListViewModel
            {
                PaginatedMovies = paginatedMovies
            };

            if (User.Identity.IsAuthenticated)
            {
                var seenMovies = _seenRepository.GetSeenMoviesByUser(User.Identity.Name).ToList();
                ViewData["seenMovies"] = seenMovies;
            }            
            
            return View(viewmodel);
        }

        public ActionResult Details(int id)
        {
            Movie movie = _movieRepository.GetMovie(id);

            /* Getting the poster image (not working)
            string url = "http://www.imdb.com/title/" + movie.Link;
            string strResult = "";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if ((request.HaveResponse) && (response.StatusCode == HttpStatusCode.OK))
            {
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    strResult = sr.ReadToEnd();
                    sr.Close();
                }
            }

            HtmlDocument ContentHtml = new HtmlDocument();
            ContentHtml.LoadHtml(strResult);
            string poster = ContentHtml.GetElementbyId("primary-poster").OuterHtml;

            ViewData["poster"] = poster;
            */

            ViewData["users"] = _seenRepository.GetUsersWhoHaveSeenMovie(movie.ID).ToList();

            ViewData["rankLog"] = _movieRepository.GetMovieRankLog(id).ToList();
 
            return View(movie);
        }

        public ActionResult Search(string query)
        {
            var movies = _movieRepository.SearchMovie(query).ToList();

            if (User.Identity.IsAuthenticated)
            {
                var seenMovies = _seenRepository.GetSeenMoviesByUser(User.Identity.Name).ToList();
                ViewData["seenMovies"] = seenMovies;
            }

            ViewData["query"] = query;

            return View(movies);
        }

    }
}
