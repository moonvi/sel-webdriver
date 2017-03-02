using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litecart
{
    public class MainPage
    {
        private const string HomeUrl= "http://localhost/litecart";
        private const string CheckoutXpath = "//div[@id='cart']/a[@class='link']";
        private const string OrderSummaryXpath = "//h2[@class='title']";
        private const string CartQuantityXpath = "//div[@id='cart']/a[2]/span[contains(@class,'quantity')]";
        private const string PopularProductXpath = "//div[@id='box-most-popular']//li[{0}]/a[1]";
        private const string ProductNameXpath = "//h1[@itemprop='name']";

        private IWebDriver _driver;
        private WebDriverWait wait;

        public MainPage(IWebDriver driver)
        {
            _driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void OpenMainPage()
        {
            _driver.Url = HomeUrl;
        }

        public int GetCartQuantity()
        {
            IWebElement cartQuantity = _driver.FindElement(By.XPath(CartQuantityXpath));
            string defaultQuantity = cartQuantity.GetAttribute("outerText");
            return int.Parse(defaultQuantity);
        }

        public CartPage NavigateToCartPage()
        {
            IWebElement cart = _driver.FindElement(By.XPath(CheckoutXpath));
            cart.Click();
            wait.Until(ExpectedConditions.ElementExists(By.XPath(OrderSummaryXpath)));

            return new CartPage(_driver);
        }

        public ProductPage OpenMostPopularProduct(int index)
        {
            IWebElement firstPopularProduct;
            string firstPopularProductLocalor = string.Format(PopularProductXpath, index);
            firstPopularProduct = _driver.FindElement(By.XPath(firstPopularProductLocalor));
            firstPopularProduct.Click();
            wait.Until(ExpectedConditions.ElementExists(By.XPath(ProductNameXpath)));

            return new ProductPage(_driver);
        }

    }
}
