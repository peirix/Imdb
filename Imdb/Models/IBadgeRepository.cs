using System;
namespace Imdb.Models
{
    public interface IBadgeRepository
    {
        void Add(BadgeList badgeList);
        void CheckForBadges(string user);
        System.Linq.IQueryable<Badge> GetUserBadges(string user);
        void Save();
        bool UserHasBadge(string user, int badgeID);
    }
}
