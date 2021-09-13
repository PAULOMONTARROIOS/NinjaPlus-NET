using NinjaPlus.Common;
using NinjaPlus.Lib;
using NinjaPlus.Models;
using NinjaPlus.Pages;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NinjaPlus.Tests
{
    public class SaveMovieTest : BaseTest
    {
        private LoginPage _login;
        private MoviePage _movie;

        [SetUp]
        public void Before()
        {
            _login = new LoginPage(Browser);
            _movie = new MoviePage(Browser);
            _login.With("paulo@paulo.com", "12345");
        }

        [Test]
        public void ShouldSaveMovie()
        {
            var movieData = new MovieModel()
            {
                Title = "Resident Evil",
                Status = "Disponível",
                Year = 2002,
                ReleaseDate = "01/05/2002",
                Cast = { "Paulo", "Glen", "Show", "Mila" },
                Plot = "A missão da missão do esquadrão e da Alice é vender a rainha vermelha.",
                Cover = CoverPath() + "resident.jpg"
            };
            Database.RemoveByTitle(movieData.Title);

            _movie.Add();
            _movie.Save(movieData);

            Assert.That(_movie.HasMovie(movieData.Title), $"Erro ao veficiar se o filme {movieData.Title} estava cadastrado.");
        }

        
    }
}
