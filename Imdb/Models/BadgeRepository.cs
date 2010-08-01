using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Imdb.Models;

namespace Imdb.Models
{
    public class BadgeRepository : IBadgeRepository
    {
        ImdbDataContext db = new ImdbDataContext();

        //Queries
        public IQueryable<Badge> GetUserBadges(string user)
        {
            return from badge in db.Badges
                   join l in db.BadgeLists
                   on badge.ID equals l.BadgeID
                   where l.UserID == user
                   select badge;
        }

        public bool UserHasBadge(string user, int badgeID)
        {
            var tempBadge = from badge in db.Badges
                            join list in db.BadgeLists
                            on badge.ID equals list.BadgeID
                            where list.UserID == user & list.BadgeID == badgeID
                            select badge;
            return (tempBadge.Count() > 0);
        }

        //Insert/Delete
        public void Add(BadgeList badgeList)
        {
            if (!UserHasBadge(badgeList.UserID, badgeList.BadgeID))
                db.BadgeLists.InsertOnSubmit(badgeList);
        }

        //Save
        public void Save()
        {
            db.SubmitChanges();
        }

        //Handle badges
        public void CheckForBadges(string user)
        {
            BadgeRepository badgeRep = new BadgeRepository();
            MovieRepository movieRep = new MovieRepository();
            List<Movie> userMovies = movieRep.GetMoviesByUser(user).ToList();
            List<Movie> movies = db.Movies.ToList();

            //1 = seen 100
            //2 = seen 50
            //3 = seen 250
            //4 = seen top 20
            //5 = seen top 10
            //6 = seen top 50
            if (userMovies.Count() >= 50)
            {
                BadgeList badgeList = new BadgeList { BadgeID = 2, UserID = user };
                badgeRep.Add(badgeList);
                badgeRep.Save();
            }
            if (userMovies.Count() >= 100)
            {
                BadgeList badgeList = new BadgeList { BadgeID = 1, UserID = user };
                badgeRep.Add(badgeList);
                badgeRep.Save();
            }
            if (userMovies.Count() == 250)
            {
                BadgeList badgeList = new BadgeList { BadgeID = 3, UserID = user };
                badgeRep.Add(badgeList);
                badgeRep.Save();
            }

            int i = 1;
            foreach (var movie in userMovies)
            {
                if (movie.Rank == i)
                {
                    if (i == 10)
                    {
                        BadgeList badgeList = new BadgeList { BadgeID = 5, UserID = user };
                        badgeRep.Add(badgeList);
                        badgeRep.Save();
                    }
                    if (i == 20)
                    {
                        BadgeList badgeList = new BadgeList { BadgeID = 4, UserID = user };
                        badgeRep.Add(badgeList);
                        badgeRep.Save();
                    }
                    if (i == 50)
                    {
                        BadgeList badgeList = new BadgeList { BadgeID = 6, UserID = user };
                        badgeRep.Add(badgeList);
                        badgeRep.Save();
                    }
                }
                else
                {
                    break;
                }
                i++;
            }

            
        }
    }
}
