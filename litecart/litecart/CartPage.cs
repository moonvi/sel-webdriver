using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litecart
{
    public class CartPage
    {
        private const string MiniItemsInCartXpath = "//ul[contains(@class,'shortcuts')]/li[contains(@class,'shortcut')]";
        private const string MiniItemsPanelXpath = "//ul[contains(@class,'shortcuts')]";
        private const string FirstMiniItemInCartXpath = "//ul[contains(@class,'shortcuts')]/li[1]/a";
        private const string RemoveButtonXpath = "//div[contains(@class,'viewport')]/ul/li[1]//button[@name='remove_cart_item']";
        private const string BackLinkXpath = "//a[contains(text(),'<< Back')]";
        private const string CartQuantityXpath = "//div[@id='cart']/a[2]/span[contains(@class,'quantity')]";

        //item name in basket
        const string CartItemNameXpath = "//div[contains(@class,'viewport')]/ul/li[1]//strong";

        //item name in Order Summary
        const string ProductNameInSummaryXpathFormat = "//table/tbody//td[contains(text(),'{0}')]";

        private IWebDriver _driver;
        private WebDriverWait wait;

        public CartPage(IWebDriver driver)
        {
            _driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void RemoveAllProducts()
        {
            int miniItemsQty = _driver.FindElements(By.XPath(MiniItemsInCartXpath)).Count;
            int j = 1;

            while (j <= miniItemsQty)
            {
                if (j < miniItemsQty)
                {
                    IWebElement firstItemInCart = _driver.FindElement(By.XPath(FirstMiniItemInCartXpath));
                    firstItemInCart.Click();
                    wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(MiniItemsPanelXpath)));
                }

                string cartProductName = _driver.FindElement(By.XPath(CartItemNameXpath)).GetAttribute("outerText");
                string productNameInSummaryXpath = string.Format(ProductNameInSummaryXpathFormat, cartProductName);

                IWebElement removeButton = _driver.FindElement(By.XPath(RemoveButtonXpath));
                removeButton.Click();

                if (j < miniItemsQty)
                {
                    wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath(productNameInSummaryXpath)));
                }
                j++;
            }
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(BackLinkXpath)));
        }

        public int GetCartQuantity()
        {
            IWebElement cartQuantity = _driver.FindElement(By.XPath(CartQuantityXpath));
            string defaultQuantity = cartQuantity.GetAttribute("outerText");
            return int.Parse(defaultQuantity);
        }
    }
}
