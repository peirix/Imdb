using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Imdb.Models;
using Imdb.Controllers;
using System.Web.Mvc;
using Imdb.ViewModels;
using Imdb.Tests.Fakes;
using Moq;
using System.Collections;
using Imdb.Tests.Helpers;

namespace Imdb.Tests.Controllers
{
    [TestClass]
    public class UserControllerTest
    {
        private FakeMovieRepository movieRepository;
        private FakeSeenRepository seenRepository;
        private FakeBadgeRepository badgeRepository;
        private UserController controller;

        [TestInitialize]
        public void Setup()
        {
            movieRepository = new FakeMovieRepository();
            seenRepository = new FakeSeenRepository();
            badgeRepository = new FakeBadgeRepository();
            controller = new UserController(movieRepository, seenRepository, badgeRepository);

            MockUserAuthentication(true);
        }

        [TestMethod]
        public void Logged_In_Users_Can_See_Their_Profile()
        {
            var viewmodel = controller.Index("user").GetViewModel<UserIndexViewModel>();

            Assert.IsNotNull(viewmodel);
        }

        [TestMethod]
        public void Not_Logged_In_Cannot_See_Profile()
        {
            MockUserAuthentication(false);
            var viewmodel = controller.Index("user") as ViewResult;

            viewmodel.ViewName.ShouldEqual("LogOn");
        }

        [TestMethod]
        public void Users_Should_See_100_Seen_Movies()
        {
            var viewmodel = controller.Index("user").GetViewModel<UserIndexViewModel>();

            viewmodel.MovieList.Movies.Count().ShouldEqual(100);
        }

        [TestMethod]
        public void Users_Should_See_1_Badge()
        {
            var viewmodel = controller.Index("user").GetViewModel<UserIndexViewModel>();

            viewmodel.Badges.Count().ShouldEqual(1);
        }




        private void MockUserAuthentication(bool authenticated)
        {
            var mock = new Mock<ControllerContext>();
            mock.SetupGet(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(authenticated);
            controller.ControllerContext = mock.Object;
        }
    }
}
