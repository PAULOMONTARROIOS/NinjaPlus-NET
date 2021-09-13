using NinjaPlus.Common;
using NinjaPlus.Pages;
using NUnit.Framework;

namespace NinjaPlus.Tests
{
    public class LoginTestes : BaseTest
    {
        public LoginPage loginPage;
        public SideBar sideBar;

        [SetUp]
        public void Before()
        {
            loginPage = new LoginPage(Browser);
            sideBar = new SideBar(Browser);
        }

        [Test]
        [Category("Smoke")]
        public void ShouldSeeLoggedUser()
        {
            loginPage.Load();
            loginPage.With("paulo@paulo.com","12345");

            Assert.AreEqual("Paulo", sideBar.LoggedUser());
        }

        [Test]
        public void ShouldSeeIncorrectPass()
        {
            loginPage.Load();
            loginPage.With("paulo@paulo.com", "123456");

            Assert.AreEqual("Usuário e/ou senha inválidos", loginPage.MessageFeedbackLogin());
        }

        [Test]
        public void ShouldSeeRequiredEmail()
        { 
            loginPage.Load();
            loginPage.With("", "12345");

            Assert.AreEqual("Opps. Cadê o email?", loginPage.MessageFeedbackLogin());
        }


        [Test]
        public void ShouldSeeRequiredPassword()
        {
            loginPage.Load();
            loginPage.With("paulo@paulo.com", "");

            Assert.AreEqual("Opps. Cadê a senha?", loginPage.MessageFeedbackLogin());
        }


    }
}
