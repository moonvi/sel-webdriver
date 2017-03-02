using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.Events;

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
            return GetVisibleElementsCount(el, locator) > 0;
        }

        protected static bool TryFindElement(IWebDriver driver, By locator, out IWebElement el)
        {
            el = null;
            try
            {
                el = driver.FindElement(locator);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        protected static bool TryFindElement(IWebElement context, By locator, out IWebElement el)
        {
            el = null;
            try
            {
                el = context.FindElement(locator);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        protected static int GetVisibleElementsCount(IWebElement el, By locator)
        {
            IList<IWebElement> subElements = el.FindElements(locator);
            int subElQty = 0;

            foreach (IWebElement subEl in subElements)
            {
                if (subEl.Displayed)
                {
                    subElQty++;
                }
            }
            return subElQty;
        }

        protected static void FillAutoComplete(IWebElement el, By inputActivatorLocator, By searchLocator, By selectItemLocator, string value)
        {
            el.FindElement(inputActivatorLocator).Click();
            el.FindElement(searchLocator).SendKeys(value);
            el.FindElement(selectItemLocator).Click();
        }

        protected void SelectDropdownValue(IWebElement el, By dropdownLocator, By selectItemLocator)
        {
            el.FindElement(dropdownLocator).Click();
            wait.Until(ExpectedConditions.ElementExists(selectItemLocator));
            el.FindElement(selectItemLocator).Click();
        }

        protected void SelectDropdownValue(IWebDriver driver, By dropdownLocator, By selectItemLocator)
        {
            driver.FindElement(dropdownLocator).Click();
            wait.Until(ExpectedConditions.ElementExists(selectItemLocator));
            driver.FindElement(selectItemLocator).Click();
        }

        protected string ThereIsWindowOtherThan(IReadOnlyList<string> oldWindows)
        {
            string newWindow = null;
            
            IReadOnlyList<string> allWindowHandles = driver.WindowHandles;

            for (int i = 0; i < allWindowHandles.Count; i++)
            {
                if (!Contains(allWindowHandles[i], oldWindows))
                {
                    newWindow = allWindowHandles[i];
                    break;
                }
            }
            return newWindow;
        }

        public bool Contains(string el, IReadOnlyList<string> collection)
        {
            bool contains = false;

            for (int j = 0; j < collection.Count; j++)
            {
                if (el.Equals(collection[j]))
                {
                    contains = true;
                    break;
                }
            }
            return contains;
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
