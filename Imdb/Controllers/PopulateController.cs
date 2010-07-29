using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using HtmlAgilityPack;
using Imdb.Models;

namespace Imdb.Controllers
{
    public class PopulateController : Controller
    {
        MovieRepository movieRep = new MovieRepository();


        //
        // GET: /Populate

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Temp()
        {
            int i = 1;
            for (i = 1; i < 251; i++)
            {
                Movie movie = movieRep.GetMovie(i+37);
                movie.Rank = i;
                movieRep.Save();
            }

            return Content("Fixed");
        }

        //
        // GET: /Populate/ShowList
        public ActionResult ShowList()
        {
            var movies = GetTop250Table();

            return View(movies);
        }

        //
        // GET: /Populate/Log

        public ActionResult Log()
        {
            var oldMovies = movieRep.AllMovies();
            var newMovies = GetTop250Table();
            foreach (var movie in newMovies)
            {
                var oldMovie = oldMovies.SingleOrDefault(m => m.Link == movie.Link);
                if (oldMovie == null)
                {
                    //This movie is new to the list
                    movieRep.Add(movie);
                }
                else
                {
                    //Log the old rank for the movie
                    movieRep.LogMovie(oldMovie);
                    //Update with new info
                    oldMovie.Rank = movie.Rank;
                    oldMovie.Rating = movie.Rating;
                    oldMovie.Votes = movie.Votes;
                    //Save the db
                    movieRep.Save();
                }
            }
            return View();
        }

        //
        // GET: /Populate/PopulateData

        public ActionResult PopulateData()
        {
            var movies = GetTop250Table();

            foreach (var movie in movies)
            {
                movieRep.Add(movie);
                movieRep.Save();
            }


            return View();
        }



        private List<Movie> GetTop250Table()
        {
            string url = "http://www.imdb.com/chart/top";
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
            HtmlNode table = ContentHtml.DocumentNode.SelectSingleNode("//table");
            table.RemoveChild(table.SelectSingleNode("tr[1]"));

            List<Movie> movies = new List<Movie>();

            foreach (HtmlNode row in table.SelectNodes("//tr"))
            {
                try
                {
                    string rankStr = row.SelectSingleNode("td[1]").InnerText;
                    rankStr = rankStr.Substring(0, rankStr.Length - 1);
                    int rank = Convert.ToInt16(rankStr);

                    int tempRating = Convert.ToInt16(row.SelectSingleNode("td[2]").InnerText.Replace(".", ""));
                    double rating = (double)tempRating / 10;

                    string link = row.SelectSingleNode("descendant::a").GetAttributeValue("href", "#").ToString();
                    link = link.Substring(7, 9);

                    string name = row.SelectSingleNode("td[3]").InnerText;
                    int year = Convert.ToInt16(name.Substring(name.IndexOf("(") + 1, 4));

                    name = name.Substring(0, name.IndexOf(" ("));

                    int votes = Convert.ToInt32(row.SelectSingleNode("td[4]").InnerText.Replace(",", ""));

                    movies.Add(new Movie
                    {
                        Rank = rank,
                        Rating = rating,
                        Link = link,
                        Name = name,
                        ReleaseYear = year,
                        Votes = votes
                    });
                }
                catch
                {
                    //hmmm...
                }
                
            }

            return movies;
        }

    }
}
