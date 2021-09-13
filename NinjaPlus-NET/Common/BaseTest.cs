using Coypu;
using Coypu.Drivers.Selenium;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NinjaPlus.Common
{
    public class BaseTest
    {
        protected BrowserSession Browser;

        [SetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder().AddJsonFile("config.json").Build();
            var sessionConfig = new SessionConfiguration
            {
                AppHost = "http://ninjaplus-web",
                Port = 5000,
                SSL = false,
                Driver = typeof(SeleniumWebDriver),
                Timeout = TimeSpan.FromSeconds(Convert.ToDouble(config["timeout"]))
            };

            if (config["browser"].Equals("chrome"))
            {
                sessionConfig.Browser = Coypu.Drivers.Browser.Chrome;
            }
            else if (config["browser"].Equals("firefox"))
            {
                sessionConfig.Browser = Coypu.Drivers.Browser.Firefox;
            }

            Browser = new BrowserSession(sessionConfig);
            Browser.MaximiseWindow();
        }

        public string CoverPath()
        {
            return Environment.CurrentDirectory + "\\Images\\";
        }

        public void TakeScreenshot()
        {
            var resultId = TestContext.CurrentContext.Test.ID;
            var shotPath = Environment.CurrentDirectory + "\\screeshots";

            if (!Directory.Exists(shotPath))
            {
                Directory.CreateDirectory(shotPath);
            }

            Browser.SaveScreenshot($"{shotPath}\\{resultId}.png");
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                TakeScreenshot();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro ao capturar o screenshot");
                throw new Exception(ex.Message);
            }
            finally
            {
                Browser.Dispose();
            }
        }

    }
}
