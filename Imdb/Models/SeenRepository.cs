using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Imdb.Models;

namespace Imdb.Models
{
    public class SeenRepository : ISeenRepository
    {
        ImdbDataContext db = new ImdbDataContext();

        //Queries
        public IQueryable<int> GetSeenMoviesByUser(string user)
        {
            return from seen in db.Seens
                   where seen.UserID == user
                   orderby seen.MovieID
                   select seen.MovieID;
        }

        public Seen GetSeen(int id, string user)
        {
            return db.Seens.SingleOrDefault(s => s.MovieID == id & s.UserID == user);
        }

        public IQueryable<string> GetUsersWhoHaveSeenMovie(int id)
        {
            return from seen in db.Seens
                   where seen.MovieID == id
                   select seen.UserID;
        }

        //Insert/Delete
        public void Add(Seen seen)
        {
            Seen tempSeen = GetSeen(seen.MovieID, seen.UserID);
            if (tempSeen == null)
            {
                db.Seens.InsertOnSubmit(seen);
            }
        }

        public void Delete(Seen seen)
        {
            db.Seens.DeleteOnSubmit(seen);
        }

        public void Save()
        {
            db.SubmitChanges();
        }
    }
}