using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Litecart
{
    public class TestBase
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;

        protected static bool IsElementExistsAndVisible(IWebDriver driver, By locator)
        {
            try
            {
                IWebElement el = driver.FindElement(locator);

                return el.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        protected static bool IsElementExistsAndVisible(IWebElement el, By locator)
        {
            try
            {
                el.FindElement(locator);

                return el.Displayed;
            }
            catch
            {
                return false;
            }
        }

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
