﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imdb.Models;

namespace Imdb.Tests.Fakes
{
    public class FakeSeenRepository: ISeenRepository
    {
        public List<int> SeenMovies;

        public void Add(Seen seen)
        {
            Seen tempSeen = GetSeen(seen.MovieID, seen.UserID);
            if (tempSeen == null)
            {
                SeenMovies.Add(seen.MovieID);
            }
        }

        public void Delete(Seen seen)
        {
            if (SeenMovies.Contains(seen.ID))
                SeenMovies.Remove(seen.ID);
        }

        public Seen GetSeen(int id, string user)
        {
            if (SeenMovies.Contains(id))
                return new Seen { MovieID = id, UserID = user };
            else
                return null;
        }

        public IQueryable<int> GetSeenMoviesByUser(string user)
        {
            return SeenMovies.AsQueryable();
        }

        public IQueryable<string> GetUsersWhoHaveSeenMovie(int id)
        {
            List<string> seenBy = new List<string> { "user1", "user2" };
            return seenBy.AsQueryable();
        }

        public void Save()
        {
            //do nothing..?
        }
    }
}
