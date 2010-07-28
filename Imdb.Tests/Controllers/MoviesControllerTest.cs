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

            MockUserAuthentication(false);
        }

        [TestMethod]
        public void Displays_20_Movies_On_Load()
        {
            var viewModel = controller.Index(null, null).GetViewModel<MovieListViewModel>();

            viewModel.PaginatedMovies.Count().ShouldEqual(20);
        }

        [TestMethod]
        public void Can_Choose_Paging_Options()
        {
            var viewModel = controller.Index(null, null).GetViewModel<MovieListViewModel>();            
            viewModel.PageSizeOptions.ShouldContain(new int[] {10, 20, 50, 100, 250});
        }

        [TestMethod]
        public void Logged_In_Users_See_Which_Movies_Are_Seen()
        {
            MockUserAuthentication(true);
            seenRepository.SeenMovies = new List<int> { 1, 2, 3 };            
            var viewModel = controller.Index(null, null).GetViewModel<MovieListViewModel>();

            viewModel.SeenMovies.ShouldContain(new int[] { 1, 2, 3 });
        }



        private void MockUserAuthentication(bool authenticated)
        {
            var mock = new Mock<ControllerContext>();
            mock.SetupGet(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(authenticated);
            controller.ControllerContext = mock.Object;
        }
    }
    
    public static class ActionResultExtensions
    {
        public static T GetViewModel<T>(this ActionResult ar)
        {
            return (T)((ViewResult)ar).ViewData.Model;
        }
    }

    public static class TestExtensions
    {
        public static void ShouldEqual(this object objA, object objB)
        {
            Assert.AreEqual(objB, objA);
        }

        public static void ShouldBeTrue(this bool check)
        {
            Assert.IsTrue(check);
        }

        public static void ShouldContain(this IList collection, object item)
        {
            int index = collection.IndexOf(item);
            if (index == -1)
                Assert.Fail("Could not find expected object in list");
        }

        public static void ShouldContain(this IList collection, IEnumerable items)
        {
            foreach(var item in items)
            {
                int index = collection.IndexOf(item);
                if (index == -1)
                    Assert.Fail("Could not find expected object in list\r\n" + item.ToString());
            }
        }
    }
}
