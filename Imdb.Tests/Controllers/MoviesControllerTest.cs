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

namespace Imdb.Tests.Controllers
{
    [TestClass]
    public class MoviesControllerTest
    {
        private FakeMovieRepository movieRepository;
        private FakeSeenRepository seenRepository;
        private MoviesController controller;

        [TestInitialize]
        public void Setup()
        {            
            movieRepository = new FakeMovieRepository();
            seenRepository = new FakeSeenRepository();
            controller = new MoviesController(movieRepository, seenRepository);

            var mock = new Mock<ControllerContext>();
            mock.SetupGet(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(false);

            controller.ControllerContext = mock.Object;
        }

        [TestMethod]
        public void Displays_20_Movies_On_Load()
        {
            var viewModel = controller.Index(null, null).GetViewModel<MovieListViewModel>();

            Assert.AreEqual(20, viewModel.PaginatedMovies.Count());
        }
    }

    public static class ActionResultExtensions
    {
        public static T GetViewModel<T>(this ActionResult ar)
        {
            return (T)((ViewResult)ar).ViewData.Model;
        }
    }
}
