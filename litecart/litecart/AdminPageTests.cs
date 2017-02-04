using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace Litecart
{
    [TestFixture]
    public class AdminPageTests : TestBase
    {
        
        [Test]
        public void CheckMenuHeadersPresented()
        {
            const string InitialMainMenuItemXpath = "//ul[@id='box-apps-menu']/li";
            const string NextMainMenuItemXpath = "//ul[@id='box-apps-menu']/li[@class='selected']/following-sibling::li[1]";
            const string NextSubMenuItemXpath = "//ul[@class='docs']/li[@class='selected']/following-sibling::li[1]";
            const string HeaderCssSelector = "td#content h1";

            LoginPage UserLogin = new LoginPage(driver);

            UserLogin.Login(Settings.AdminName, Settings.AdminPassword, true);

            string currentXPath = InitialMainMenuItemXpath;

            do
            {
                IWebElement menuItem = driver.FindElement(By.XPath(currentXPath));
                menuItem.Click();
                wait.Until(ExpectedConditions.ElementExists(By.CssSelector(HeaderCssSelector)));

                string nextSubMenuXpath = NextSubMenuItemXpath;

                while (IsElementExistsAndVisible(By.XPath(nextSubMenuXpath), driver))
                {
                    IWebElement subEl = driver.FindElement(By.XPath(nextSubMenuXpath));
                    subEl.Click();
                    wait.Until(ExpectedConditions.ElementExists(By.CssSelector(HeaderCssSelector)));
                }

                currentXPath = NextMainMenuItemXpath;
            } while (IsElementExistsAndVisible(By.XPath(currentXPath), driver));
        }
    }
}
