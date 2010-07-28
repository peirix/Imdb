using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Imdb.Models;

namespace Imdb.Controllers
{
    public class SeenController : Controller
    {
        SeenRepository seenRepository = new SeenRepository();

        //
        // AJAX: /Seen/SeenMovie/1
        [Authorize, AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SeenMovie(int id)
        {
            Seen seen = new Seen();
            seen.MovieID = id;
            seen.UserID = User.Identity.Name;

            seenRepository.Add(seen);
            seenRepository.Save();

            BadgeRepository badgeRepository = new BadgeRepository();
            badgeRepository.CheckForBadges(User.Identity.Name);

            return Content("Got it!");
        }

        [Authorize, AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UnSeenMovie(int id)
        {
            Seen seen = seenRepository.GetSeen(id, User.Identity.Name);

            seenRepository.Delete(seen);
            seenRepository.Save();

            return Content("Glad you're honest!");
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}
