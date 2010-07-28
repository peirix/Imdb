using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imdb.Models;

namespace Imdb.Tests.Fakes
{
    class FakeSeenRepository: ISeenRepository
    {
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
            throw new NotImplementedException();
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
