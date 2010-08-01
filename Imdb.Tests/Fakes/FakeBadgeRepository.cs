using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imdb.Models;

namespace Imdb.Tests.Fakes
{
    public class FakeBadgeRepository : IBadgeRepository
    {
        public void Add(BadgeList badgeList)
        {
            throw new NotImplementedException();
        }

        public void CheckForBadges(string user)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Badge> GetUserBadges(string user)
        {
            List<Badge> badges = new List<Badge>();
            badges.Add(new Badge
            {
                Name = "TestBadge",
                ID = 1
            });
            return badges.AsQueryable();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public bool UserHasBadge(string user, int badgeID)
        {
            throw new NotImplementedException();
        }
    }
}
