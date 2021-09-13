using Coypu;
using NinjaPlus.Common;
using NinjaPlus.Pages;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace NinjaPlus.Tests
{
    public class CenariosDDT : BaseTest
    {
        public LoginPage loginPage;
        public SideBar sideBar;

        [SetUp]
        public void Start()
        {
            loginPage = new LoginPage(Browser);
            sideBar = new SideBar(Browser);
        }

        //[TestCase("paulo@paulo.com","123456","Paulo")]
        //[TestCase("paulo@paulo.com", "123456", "Usuário e/ou senha inválidos")]
        //[TestCase("paulo@ricardo.com", "12345", "Usuário e/ou senha inválidos")]
        //[TestCase("", "123456", "Opps. Cadê o email?")]
        //[TestCase("paulo@paulo.com", "", "Opps. Cadê a senha?")]
        public void Test(string email, string pass, string expectMessage)
        {
            loginPage.Load();
            loginPage.With(email, pass);

            Assert.AreEqual(expectMessage, sideBar.LoggedUser());
        }

    }
}
