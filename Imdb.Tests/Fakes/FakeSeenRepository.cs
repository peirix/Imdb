using System;
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
            throw new NotImplementedException();
        }

        public void Delete(Seen seen)
        {
            throw new NotImplementedException();
        }

        public Seen GetSeen(int id, string user)
        {
            throw new NotImplementedException();
        }

        public IQueryable<int> GetSeenMoviesByUser(string user)
        {
            return SeenMovies.AsQueryable();
        }

        public IQueryable<string> GetUsersWhoHaveSeenMovie(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
