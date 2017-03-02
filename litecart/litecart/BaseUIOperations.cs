using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litecart
{
    public class BaseUIOperations
    {
        private IWebDriver _driver;
        private WebDriverWait wait;

        public BaseUIOperations(IWebDriver driver)
        {
            _driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }
        public bool IsElementExistsAndVisible(By locator)
        {
            try
            {
                IWebElement el = _driver.FindElement(locator);
                return el.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        public void SelectDropdownValue(IWebDriver driver, By dropdownLocator, By selectItemLocator)
        {
            driver.FindElement(dropdownLocator).Click();
            wait.Until(ExpectedConditions.ElementExists(selectItemLocator));
            driver.FindElement(selectItemLocator).Click();
        }
    }
}
