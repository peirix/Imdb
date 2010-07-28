using System;
namespace Imdb.Models
{
    public interface ISeenRepository
    {
        void Add(Seen seen);
        void Delete(Seen seen);
        Seen GetSeen(int id, string user);
        System.Linq.IQueryable<int> GetSeenMoviesByUser(string user);
        System.Linq.IQueryable<string> GetUsersWhoHaveSeenMovie(int id);
        void Save();
    }
}
