using Coypu;
using System;
using System.Collections.Generic;
using System.Text;

namespace NinjaPlus.Pages
{
    public class LoginPage
    {
        private readonly BrowserSession _browser;

        public LoginPage(BrowserSession browser)
        {
            _browser = browser;
        }

        public void Load()
        {
            _browser.Visit("/login");
        }

        public void With(string email, string password)
        {
            this.Load();

            _browser.FillIn("emailId").With(email);
            //var inputEmail = browser.FindId("emailId");
            //inputEmail.SendKeys("paulo@paulo.com");


            _browser.FillIn("passId").With(password);
            // browser.FindCss("input[placeholder='senha']").SendKeys("12345");
            //var inputPassword = browser.FindId("passId");
            //inputPassword.SendKeys("12345");

            _browser.ClickButton("Entrar");
            //browser.FindId("login").Click();
            //browser.ClickButton("Entrar"); //Busca pelo texto do botao
        }

        public string MessageFeedbackLogin()
        {
            return _browser.FindCss(".alert span b").Text;
        }
      
    }
}
