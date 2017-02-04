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

            string productXpath = "//li[@class='product column shadow hover-light']";
            string stickerXpath = "//div[contains(@class,'sticker')]";
           
            IList<IWebElement> products = driver.FindElements(By.XPath(productXpath));

            bool allStickersPresented = true;

            foreach (IWebElement item in products)
            {
                if (!IsElementExistsAndVisible(item, By.XPath(stickerXpath)))
                {
                    allStickersPresented = false;
                    break;
                }
            }
            Assert.IsTrue(allStickersPresented);
        }
    }
}
