using NUnit.Framework;
using OpenQA.Selenium;
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
        [Test]
        public void CheckStickersPresented()
        {
            MainPage HomePage = new MainPage(driver);
            HomePage.OpenMainPage();

            string productXpath = "//li[contains(@class,'product')]";
            string stickerXpath = ".//div[contains(@class,'sticker')]";
           
            IList<IWebElement> products = driver.FindElements(By.XPath(productXpath));

            bool allProductsHaveOneSticker = true;

            foreach (IWebElement item in products)
            {
                if (GetVisibleElementsCount(item,By.XPath(stickerXpath))!=1)
                {
                    allProductsHaveOneSticker = false;
                    break;
                }
            }
            Assert.IsTrue(allProductsHaveOneSticker);
        }
    }
}
