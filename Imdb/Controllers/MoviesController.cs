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

namespace Imdb.Controllers
{
    public class MoviesController : Controller
    {
        MovieRepository movieRepository = new MovieRepository();
        SeenRepository seenRepository = new SeenRepository();
        //
        // GET: /Movie/

        public ActionResult Index(int? page, int? pageSize)
        {
            var movies = movieRepository.AllMovies();
            var paginatedMovies = new PaginatedList<Movie>(movies, page ?? 0, pageSize ?? 20);

            if (User.Identity.IsAuthenticated)
            {
                var seenMovies = seenRepository.GetSeenMoviesByUser(User.Identity.Name).ToList();
                ViewData["seenMovies"] = seenMovies;
            }

            List<int> pageSizeOptions = new List<int>();
            pageSizeOptions.Add(10);
            pageSizeOptions.Add(20);
            pageSizeOptions.Add(50);
            pageSizeOptions.Add(100);
            pageSizeOptions.Add(250);
            ViewData["pageSizeOptions"] = pageSizeOptions;

            return View(paginatedMovies);
        }

        public ActionResult Details(int id)
        {
            Movie movie = movieRepository.GetMovie(id);

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

            ViewData["users"] = seenRepository.GetUsersWhoHaveSeenMovie(movie.ID).ToList();

            ViewData["rankLog"] = movieRepository.GetMovieRankLog(id).ToList();
 
            return View(movie);
        }

        public ActionResult Search(string query)
        {
            var movies = movieRepository.SearchMovie(query).ToList();

            if (User.Identity.IsAuthenticated)
            {
                var seenMovies = seenRepository.GetSeenMoviesByUser(User.Identity.Name).ToList();
                ViewData["seenMovies"] = seenMovies;
            }

            ViewData["query"] = query;

            return View(movies);
        }

    }
}
