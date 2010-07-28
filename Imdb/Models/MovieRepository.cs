using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Imdb.Models
{
    public class MovieRepository
    {
        ImdbDataContext db = new ImdbDataContext();

        //Query methods
        public IQueryable<Movie> AllMovies()
        {
            return db.Movies;
        }

        public Movie GetMovie(int id)
        {
            return db.Movies.SingleOrDefault(m => m.ID == id);
        }

        public IQueryable<Movie> GetMoviesByUser(string user)
        {
            return from movie in db.Movies
                   join seen in db.Seens on movie.ID equals seen.MovieID into sm
                   from seenmovie in sm.DefaultIfEmpty()
                   where seenmovie.UserID == user
                   orderby movie.Rank
                   select movie;
        }

        public IQueryable<int> GetMovieRankLog(int id)
        {
            return from log in db.MovieLogs
                   where log.MovieID == id
                   select log.Rank;
        }

        public IQueryable<Movie> SearchMovie(string query)
        {
            return from movie in db.Movies
                   where movie.Name.ToLower().Contains(query.ToLower())
                   select movie;
        }

        //Insert/delete
        public void Add(Movie movie)
        {
            db.Movies.InsertOnSubmit(movie);
        }

        //Persist
        public void Save()
        {
            db.SubmitChanges();
        }
    }
}