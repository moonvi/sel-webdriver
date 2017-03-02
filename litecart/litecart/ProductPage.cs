using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litecart
{
    public class ProductPage
    {
        private const string AddToCartButtonXpath = "//button[@type='submit'][@name='add_cart_product']";
        private const string SelectElementXpath = "//select[@name='options[Size]']";
        private const string SelectItemXpath = "//option[@value='Small']";
        private const string CartQuantityXpath = "//div[@id='cart']/a[2]/span[contains(@class,'quantity')]";
        private const string HomePageXpath = "//nav[@id='site-menu']//li[contains(@class,'general-0')]";
        private const string MostPopularXpath = "//div[@id='box-most-popular']";

        private string nextItemCartQuantity = "";

        private IWebDriver _driver;
        private WebDriverWait wait;
        private BaseUIOperations _uiOperations;

        public ProductPage(IWebDriver driver)
        {
            _driver = driver;
            _uiOperations = new BaseUIOperations(_driver);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void AddToCartWithDefailtSettings(int index)
        {
            IWebElement addToCartButton = _driver.FindElement(By.XPath(AddToCartButtonXpath));

            if (_uiOperations.IsElementExistsAndVisible(By.XPath(SelectElementXpath)))
            {
                _uiOperations.SelectDropdownValue(_driver,By.XPath(SelectElementXpath), By.XPath(SelectItemXpath));
            }
            addToCartButton.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(CartQuantityXpath)));

            IWebElement updatedCartQuantity = _driver.FindElement(By.XPath(CartQuantityXpath));
            wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath(CartQuantityXpath), index.ToString()));
            //nextItemCartQuantity = updatedCartQuantity.GetAttribute("outerText");
        }

        public void NavigateToTheMainPage()
        {
            IWebElement generalMenu = _driver.FindElement(By.XPath(HomePageXpath));
            generalMenu.Click();
            wait.Until(ExpectedConditions.ElementExists(By.XPath(MostPopularXpath)));
        }
    }
}
