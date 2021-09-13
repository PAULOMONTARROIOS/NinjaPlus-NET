using Coypu;
using NinjaPlus.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace NinjaPlus.Pages
{
    public class MoviePage
    {
        private readonly BrowserSession _browser;

        public MoviePage(BrowserSession browser)
        {
            _browser = browser;
        }

        public void Add()
        {
            _browser.FindCss(".movie-add").Click();
        }

        private void SelectStatus(string status)
        {
            _browser.FindCss("input[placeholder=Status]").Click();
            _browser.FindCss("ul li span", text: status).Click();
        }

        private void InputCast(List<string> cast)
        {
            var element = _browser.FindCss("input[placeholder$=ator]");
            foreach (var actor in cast)
            {
                element.SendKeys(actor);
                element.SendKeys(Keys.Tab);
                Thread.Sleep(500); //Thinking Time
            }
        }

        private void UpdloadCover(string cover)
        {
            var jsScript = "document.getElementById('upcover').classList.remove('el-upload__input')";
            _browser.ExecuteScript(jsScript);
            Thread.Sleep(1000);
            _browser.FindCss("#upcover").SendKeys(cover);
        }

        public void Save(MovieModel movie)
        {
            _browser.FindCss("input[name=title]").SendKeys(movie.Title);
            SelectStatus(movie.Status);
            _browser.FindCss("input[name=year]").SendKeys(movie.Year.ToString());
            _browser.FindCss("input[name=release_date]").SendKeys(movie.ReleaseDate);
            InputCast(movie.Cast);
            _browser.FindCss("textarea[name=overview]").SendKeys(movie.Plot);

            UpdloadCover(movie.Cover);

            _browser.ClickButton("Cadastrar");
        }

        public void Search(string value)
        {
            _browser.FindCss("input[placeholder^=Pesquisar]").SendKeys(value);
            _browser.FindCss("#search-movie").Click();

        }

        public bool HasMovie(string title)
        {
            return _browser.FindCss("table tbody tr td", text: title).Exists();
        }

        public int CountMovie()
        {
            return _browser.FindAllCss("table tbody tr").Count();
        }

        public string SearchAlert()
        {
            return _browser.FindCss(".alert-dark").Text;
        }
    }
}
