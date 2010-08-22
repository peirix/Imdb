using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Imdb.Models;
using Imdb.Helpers;

namespace Imdb.ViewModels
{
    public class MovieList
    {
        public PaginatedList<Movie> Movies { get; set; }
        public List<int> SeenMovies { get; set; }
        public Dictionary<int, int> LastMovieRanks { get; set; }

        public void SetLastMovieRanks(IMovieRepository movieRep)
        {
            LastMovieRanks = new Dictionary<int, int>();
            foreach (var movie in Movies)
            {
                int lastLog = movieRep.GetPreviousMovieRank(movie.ID);
                LastMovieRanks.Add(movie.ID, lastLog);
            }
        }
        

        public string GetRankMove(int movieID, int rank)
        {
            int lastRank = LastMovieRanks[movieID];

            if (lastRank == 0)
                return "new";
            if (lastRank < rank)
                return "down";
            if (lastRank > rank)
                return "up";
            if (lastRank == rank)
                return "same";

            return "noidea";
        }
    }
}