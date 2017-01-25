using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;

namespace litecart
{
    [TestFixture]
    public class LoginPageTests
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void FillPageFields()
        {
            driver.Url = "http://localhost/litecart/admin/login.php";

            IWebElement usernameField = driver.FindElement(By.Name("username"));
            usernameField.SendKeys("admin");

            IWebElement passField = driver.FindElement(By.Name("password"));
            passField.SendKeys("admin");

            IWebElement rememberMeCheckbox = driver.FindElement(By.Name("remember_me"));
            rememberMeCheckbox.Click();

            IWebElement loginBtn = driver.FindElement(By.Name("login"));
            loginBtn.Click();
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
