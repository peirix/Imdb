using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imdb.Models;

namespace Imdb.Tests.Builders
{
    public class MovieBuilder
    {
        private static List<Movie> _movies;

        public static MovieBuilder Create(int numberOfMovies)
        {
            return new MovieBuilder(numberOfMovies);
        }

        public MovieBuilder WithRatingsBetween(int min, int max)
        {
            var r = new Random();
            _movies.ForEach(m => m.Rating = r.Next(min, max));
            return this;
        }

        private MovieBuilder()
        {
            _movies = new List<Movie>();
        }

        private MovieBuilder(int numberOfMovies)
        {
            _movies = new List<Movie>();

            for (int i = 0; i < numberOfMovies; ++i)
                _movies.Add(new Movie());
        }

        public static implicit operator List<Movie>(MovieBuilder builder)
        {
            return _movies;
        }        
    }
}
