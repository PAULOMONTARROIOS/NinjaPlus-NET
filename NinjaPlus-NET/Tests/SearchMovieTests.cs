using NinjaPlus.Common;
using NinjaPlus.Lib;
using NinjaPlus.Pages;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NinjaPlus.Tests
{
    public class SearchMovieTests : BaseTest
    {

        private LoginPage _login;
        private MoviePage _movie;

        [SetUp]
        public void Before()
        {
            _login = new LoginPage(Browser);
            _movie = new MoviePage(Browser);
            Database.InsertMovies();
            _login.With("paulo@paulo.com","12345");  
        }

        [Test]
        public void ShouldFindUniqueMovie()
        {
            var target = "Coringa";
            _movie.Search(target);

            Assert.That(_movie.HasMovie(target), $"Erro! O filme {target} não foi encontrado.");
            Browser.HasNoContent("Não foi entrado nada.");
            Assert.AreEqual(1, _movie.CountMovie());
        }

        [Test]
        public void ShouldFindMovies()
        {
            var target = "Batman";
            _movie.Search(target);

            Assert.That(_movie.HasMovie("Batman Begins"),
                $"Erro! Não foi encontrado nada com o filtro: 'Batman Begins'.");
            Assert.That(_movie.HasMovie("Batman O Cavaleiro das Trevas"),
                $"Erro! Não foi encontrado nada com o filtro: 'Batman O Cavaleiro das Trevas'.");

            Assert.AreEqual(2, _movie.CountMovie());
        }

        [Test]
        public void ShouldDisplayNoMovieFound()
        {
            var target = "Os trapalhões";
            _movie.Search(target);

            Assert.AreEqual("Puxa! não encontramos nada aqui :(", _movie.SearchAlert());
            Assert.AreEqual(0, _movie.CountMovie());
        }

        [OneTimeTearDown]
        public void After()
        {
            Database.DeleteMovies();     
        }

    }
}
