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
        //
        // GET: /Populate

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Temp()
        {
            MovieRepository movieRep = new MovieRepository();
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
        // GET: /Populate/Log

        public ActionResult Log()
        {
            // Scrape
            // Få alle filmer
            // For hver film, hent ut fra Movies ved sammenligning på link
            // Finn plassering.
            // Sett inn gamle plassering i MovieLog
            // Oppdater plassering i Movies
            return View();
        }

        //
        // GET: /Populate/PopulateData

        public ActionResult PopulateData()
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

            MovieRepository movieRep = new MovieRepository();

            foreach (HtmlNode row in table.SelectNodes("//tr"))
            {
                int rank = Convert.ToInt16(row.SelectSingleNode("td[1]").InnerText.Substring(0, 1));

                int tempRating = Convert.ToInt16(row.SelectSingleNode("td[2]").InnerText.Replace(".", ""));
                double rating = tempRating/10;

                string link = row.SelectSingleNode("descendant::a").GetAttributeValue("href", "#").ToString();
                link = link.Substring(7, 9);

                string name = row.SelectSingleNode("td[3]").InnerText;
                int year = Convert.ToInt16(name.Substring(name.IndexOf("(") + 1, 4));
                
                name = name.Substring(0, name.IndexOf(" ("));

                int votes = Convert.ToInt32(row.SelectSingleNode("td[4]").InnerText.Replace(",", ""));

                Movie movie = new Movie
                                {
                                    Rank = rank,
                                    Rating = rating,
                                    Link = link,
                                    Name = name,
                                    ReleaseYear = year,
                                    Votes = votes
                                };
                movieRep.Add(movie);
                movieRep.Save();
            }


            return View();
        }

    }
}
