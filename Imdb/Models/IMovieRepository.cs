using System;
namespace Imdb.Models
{
    public interface IMovieRepository
    {
        void Add(Movie movie);
        System.Linq.IQueryable<Movie> AllMovies();
        Movie GetMovie(int id);
        System.Linq.IQueryable<int> GetMovieRankLog(int id);
        System.Linq.IQueryable<Movie> GetMoviesByUser(string user);
        void Save();
        System.Linq.IQueryable<Movie> SearchMovie(string query);
    }
}
