using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imdb.Models;
using Imdb.Tests.Builders;

namespace Imdb.Tests.Fakes
{
    public class FakeMovieRepository: IMovieRepository
    {
        public void Add(Movie movie)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Movie> AllMovies()
        {
            List<Movie> movies = MovieBuilder.Create(250).WithRatingsBetween(1, 9);
            return movies.AsQueryable();
        }

        public Movie GetMovie(int id)
        {
            return new Movie { ID = id };
        }

        public IQueryable<int> GetMovieRankLog(int id)
        {
            List<int> rankLog = new List<int> { 2, 4, 5 };
            return rankLog.AsQueryable();
        }

        public int GetPreviousMovieRank(int id)
        {
            return 5; //totally random.
        }

        public IQueryable<Movie> GetMoviesByUser(string user)
        {
            List<Movie> movies = MovieBuilder.Create(100).WithRatingsBetween(1, 9);
            return movies.AsQueryable();
        }

        public DateTime LastUpdated()
        {
            return DateTime.Parse("2008.08.08");
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Movie> SearchMovie(string query)
        {
            throw new NotImplementedException();
        }
    }
}
