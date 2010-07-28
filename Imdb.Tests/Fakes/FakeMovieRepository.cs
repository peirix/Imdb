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
            throw new NotImplementedException();
        }

        public IQueryable<int> GetMovieRankLog(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Movie> GetMoviesByUser(string user)
        {
            throw new NotImplementedException();
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
