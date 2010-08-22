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
            var viewModel = controller.Index(null, null).GetViewModel<MoviesIndexViewModel>();

            viewModel.MovieList.Movies.Count().ShouldEqual(20);
        }

        [TestMethod]
        public void Can_Choose_Paging_Options()
        {
            var viewModel = controller.Index(null, null).GetViewModel<MoviesIndexViewModel>();            
            viewModel.PageSizeOptions.ShouldContain(new int[] {10, 20, 50, 100, 250});
        }

        [TestMethod]
        public void Logged_In_Users_See_Which_Movies_Are_Seen()
        {
            MockUserAuthentication(true);
            seenRepository.SeenMovies = new List<int> { 1, 2, 3 };            
            var viewModel = controller.Index(null, null).GetViewModel<MoviesIndexViewModel>();

            viewModel.MovieList.SeenMovies.ShouldContain(new int[] { 1, 2, 3 });
        }

        [TestMethod]
        public void User_Can_Mark_Movie_As_Seen()
        {
            MockUserAuthentication(true);
            seenRepository.SeenMovies = new List<int> { 1, 2, 3 };
            seenRepository.Add(new Seen { MovieID = 4 });

            var viewModel = controller.Index(null, null).GetViewModel<MoviesIndexViewModel>();

            viewModel.MovieList.SeenMovies.Count().ShouldEqual(4);
        }

        [TestMethod]
        public void User_Can_Mark_Movie_As_Not_Seen()
        {
            MockUserAuthentication(true);
            seenRepository.SeenMovies = new List<int> { 1, 2, 3, 4 };
            seenRepository.Delete(new Seen { ID = 4 });

            var viewmodel = controller.Index(null, null).GetViewModel<MoviesIndexViewModel>();

            viewmodel.MovieList.SeenMovies.Count().ShouldEqual(3);
        }

        [TestMethod]
        public void Details_View_Shows_Information_On_One_Movie()
        {
            var viewModel = controller.Details(1).GetViewModel<MovieDetailsViewModel>();

            viewModel.Movie.ID.ShouldEqual(1);
        }

        [TestMethod]
        public void Details_View_Should_Contain_List_Of_Users_Whove_Seen_The_Movie()
        {
            var viewModel = controller.Details(1).GetViewModel<MovieDetailsViewModel>();
            viewModel.SeenBy.Count().ShouldEqual(2);
        }

        [TestMethod]
        public void Front_Page_Shows_Last_Updated_Date()
        {
            var viewmodel = controller.Index(null, null).GetViewModel<MoviesIndexViewModel>();
            viewmodel.LastUpdated.ShouldEqual(DateTime.Parse("2008.08.08"));
        }



        private void MockUserAuthentication(bool authenticated)
        {
            var mock = new Mock<ControllerContext>();
            mock.SetupGet(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(authenticated);
            controller.ControllerContext = mock.Object;
        }
    }
}
