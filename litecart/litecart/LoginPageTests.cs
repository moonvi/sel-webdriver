using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.Threading;

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
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(3));
        }

        [Test]
        public void FillPageFields()
        {          
            LoginPage UserLogin = new LoginPage(driver);

            UserLogin.Login("admin", "admin", true);
        }

        [Test]
        public void CheckMenuHeadersPresented()
        {
            const string InitialMainMenuItemXpath = "//ul[@id='box-apps-menu']/li";
            const string NextMainMenuItemXpath = "//ul[@id='box-apps-menu']/li[@class='selected']/following-sibling::li[1]";
            const string NextSubMenuItemXpath = "//ul[@class='docs']/li[@class='selected']/following-sibling::li[1]";
            const string HeaderCssSelector = "td#content h1";

            LoginPage UserLogin = new LoginPage(driver);

            UserLogin.Login("admin", "admin", true);
            
            string currentXPath = InitialMainMenuItemXpath;

            do
            {
                IWebElement menuItem = driver.FindElement(By.XPath(currentXPath));
                menuItem.Click();
                wait.Until(ExpectedConditions.ElementExists(By.CssSelector(HeaderCssSelector)));

                string nextSubMenuXpath = NextSubMenuItemXpath;

                while (IsElementExistsAndVisible(nextSubMenuXpath, driver))
                {
                    IWebElement subEl = driver.FindElement(By.XPath(nextSubMenuXpath));
                    subEl.Click();
                    wait.Until(ExpectedConditions.ElementExists(By.CssSelector(HeaderCssSelector)));
                }

                currentXPath = NextMainMenuItemXpath;
            } while (IsElementExistsAndVisible(currentXPath,driver));

 /*           while (IsElementExistsAndVisible(nextMenuItemXpath, driver))
            {
                IWebElement el = driver.FindElement(By.XPath(nextMenuItemXpath));
                el.Click();
                wait.Until(ExpectedConditions.ElementExists(By.CssSelector("td#content h1")));
                
                string nextSubMenuXpath = "//ul[@class='docs']/li[@class='selected']/following-sibling::li[1]";

                while (IsElementExistsAndVisible(nextSubMenuXpath, driver))
                {
                    IWebElement subEl = driver.FindElement(By.XPath(nextSubMenuXpath));
                    subEl.Click();
                    wait.Until(ExpectedConditions.ElementExists(By.CssSelector("td#content h1")));
                }

            }*/


        }
        
        private static bool IsElementExistsAndVisible(string Xpath, IWebDriver driver)
        {
            try
            {
               IWebElement el = driver.FindElement(By.XPath(Xpath));

                return el.Displayed;
            }
            catch(NoSuchElementException)
            {
                return false;
            }
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
