using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litecart
{
    [TestFixture]
    public class MainPageTests : TestBase
    {
        private const string FirstCampaignProductXpath = "//div[@id='box-campaigns']//li[1]/a[1]";
        private const string FirstCampaignProductNameXpath = ".//div[@class='name']";
        private const string FirstCampaignRegPriceXpath = ".//s[@class='regular-price']";
        private const string FirstCampaignSpecPriceXpath = ".//strong[@class='campaign-price']";

        private const string ProductNameXpath = "//h1[@itemprop='name']";
        private const string ProductRegPriceXpath = "//div[@class='information']//*[@class='regular-price']";
        private const string ProductSpecPriceXpath = "//div[@class='information']//*[@class='campaign-price']";

        [Test]
        public void CheckStickersPresented()
        {
            MainPage HomePage = new MainPage(driver);
            HomePage.OpenMainPage();

            const string productXpath = "//li[contains(@class,'product')]";
            const string stickerXpath = ".//div[contains(@class,'sticker')]";

            IList<IWebElement> products = driver.FindElements(By.XPath(productXpath));

            bool allProductsHaveOneSticker = true;

            foreach (IWebElement item in products)
            {
                if (GetVisibleElementsCount(item, By.XPath(stickerXpath)) != 1)
                {
                    allProductsHaveOneSticker = false;
                    break;
                }
            }
            Assert.IsTrue(allProductsHaveOneSticker);
        }

        [Test]
        public void CheckCorrectProductPageOpened()
        {
            MainPage HomePage = new MainPage(driver);
            HomePage.OpenMainPage();

            IWebElement firstCampaignProduct = driver.FindElement(By.XPath(FirstCampaignProductXpath));

            string firstCampaignProductName = firstCampaignProduct.FindElement(By.XPath(FirstCampaignProductNameXpath)).Text;
            string firstCampaignRegPrice = firstCampaignProduct.FindElement(By.XPath(FirstCampaignRegPriceXpath)).Text;
            string firstCampaignSpecPriceXpath = firstCampaignProduct.FindElement(By.XPath(FirstCampaignSpecPriceXpath)).Text;

            firstCampaignProduct.Click();
            wait.Until(ExpectedConditions.ElementExists(By.XPath(ProductNameXpath)));

            string productName = driver.FindElement(By.XPath(ProductNameXpath)).Text;
            string productRegularPrice = driver.FindElement(By.XPath(ProductRegPriceXpath)).Text;
            string productSpecialPrice = driver.FindElement(By.XPath(ProductSpecPriceXpath)).Text;

            Assert.AreEqual(firstCampaignProductName, productName);
            Assert.AreEqual(firstCampaignRegPrice, productRegularPrice);
            Assert.AreEqual(firstCampaignSpecPriceXpath, productSpecialPrice);

        }

        [Test]
        public void CheckProductPriceStyles()
        {
            MainPage HomePage = new MainPage(driver);
            HomePage.OpenMainPage();

            IWebElement firstCampaignProduct = driver.FindElement(By.XPath(FirstCampaignProductXpath));

            IList<IWebElement> crossedGrayPrices = firstCampaignProduct.FindElements(By.XPath(FirstCampaignRegPriceXpath));
            Assert.IsNotEmpty(crossedGrayPrices);

            firstCampaignProduct.Click();
            wait.Until(ExpectedConditions.ElementExists(By.XPath(ProductNameXpath)));

            IList<IWebElement> strongRedPrices = driver.FindElements(By.XPath(ProductSpecPriceXpath));
            Assert.IsNotEmpty(strongRedPrices);
        }

        [Test]
        public void CheckSpecPriceGreaterThanPegular()
        {
            MainPage HomePage = new MainPage(driver);
            HomePage.OpenMainPage();

            IWebElement firstCampaignProduct = driver.FindElement(By.XPath(FirstCampaignProductXpath));

            int firstCampaignRegPriceFontHeight = firstCampaignProduct.FindElement(By.XPath(FirstCampaignRegPriceXpath)).Size.Height;
            int firstCampaignSpecPriceFontHeight = firstCampaignProduct.FindElement(By.XPath(FirstCampaignSpecPriceXpath)).Size.Height;

            Assert.Greater(firstCampaignSpecPriceFontHeight, firstCampaignRegPriceFontHeight);

            firstCampaignProduct.Click();
            wait.Until(ExpectedConditions.ElementExists(By.XPath(ProductNameXpath)));

            int productPageRegPriceFontHeight = driver.FindElement(By.XPath(ProductRegPriceXpath)).Size.Height;
            int productPageSpecPriceFontHeight = driver.FindElement(By.XPath(ProductSpecPriceXpath)).Size.Height;

            Assert.Greater(productPageSpecPriceFontHeight, productPageRegPriceFontHeight);
        }
    }
}
