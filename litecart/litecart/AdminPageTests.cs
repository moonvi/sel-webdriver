using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

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

                while (IsElementExistsAndVisible(driver, By.XPath(nextSubMenuXpath)))
                {
                    IWebElement subEl = driver.FindElement(By.XPath(nextSubMenuXpath));
                    subEl.Click();
                    wait.Until(ExpectedConditions.ElementExists(By.CssSelector(HeaderCssSelector)));
                }

                currentXPath = NextMainMenuItemXpath;
            } while (IsElementExistsAndVisible(driver, By.XPath(currentXPath)));
        }

        [Test]
        public void CheckCountriesAlphabeticallySorted()
        {
            const string CountriesMenuXpath = "//ul[@id='box-apps-menu']/li[3]";
            const string CountriesXpath = "//table[@class='dataTable']/tbody/tr[@class='row']/td[5]";

            LoginPage UserLogin = new LoginPage(driver);
            UserLogin.Login(Settings.AdminName, Settings.AdminPassword, true);

            IWebElement countriesMenu = driver.FindElement(By.XPath(CountriesMenuXpath));
            countriesMenu.Click();

            IList<IWebElement> countries = driver.FindElements(By.XPath(CountriesXpath));

            List<string> countriesNames = new List<string>();

            for (int i=0; i<countries.Count; i++)
            {
                countriesNames.Add(countries[i].Text);
            }

            bool isSortingByAsc = true;

            for (int i=0; i<countriesNames.Count-1; i++)
            {
                if(countriesNames[i].CompareTo(countriesNames[i+1])==1)
                {
                    isSortingByAsc = false;
                    break;
                }
            }
            Assert.IsTrue(isSortingByAsc);
        }

        [Test]
        public void CheckZonesAlphabeticallySorted()
        {
            const string CountriesMenuXpath = "//ul[@id='box-apps-menu']/li[3]";
            const string ZonesNotNullXpath = "//table[@class='dataTable']/tbody/tr[@class='row']/td[6][.!='0']";
            const string CountriesXpath = "../td[5]/a";
            const string ZoneNameXpath = "//table[@id='table-zones']/tbody/tr/td[3]/input[@type='hidden']";

            LoginPage UserLogin = new LoginPage(driver);
            UserLogin.Login(Settings.AdminName, Settings.AdminPassword, true);

            driver.FindElement(By.XPath(CountriesMenuXpath)).Click();

            IList<IWebElement> zones = driver.FindElements(By.XPath(ZonesNotNullXpath));

            for(int i=0; i<zones.Count;i++)
            {
                zones[i].FindElement(By.XPath(CountriesXpath)).Click();
                wait.Until(ExpectedConditions.ElementExists(By.XPath(ZoneNameXpath)));

                IList<IWebElement> zoneNames = driver.FindElements(By.XPath(ZoneNameXpath));

                bool isSortingByAsc = true;

                for (int j = 0; j < zoneNames.Count - 1; j++)
                {
                    if (zoneNames[j].GetAttribute("Value").CompareTo(zoneNames[j + 1].GetAttribute("Value")) == 1)
                    {
                        isSortingByAsc = false;
                        break;
                    }
                }
                Assert.IsTrue(isSortingByAsc);

                driver.FindElement(By.XPath(CountriesMenuXpath)).Click();
                zones = driver.FindElements(By.XPath(ZonesNotNullXpath));

            }
        }

        [Test]
        public void CheckGeoZonesAlphabeticallySorted()
        {
            const string GeoZonesMenuXpath = "//ul[@id='box-apps-menu']/li[6]";
            const string GeoZoneXpath = "//table[@class='dataTable']/tbody/tr[@class='row']/td[3]";
            const string GeoZoneNameXpath = "./a";
            const string ZoneXpath = "//table[@id='table-zones']/tbody/tr/td[3]/select/option[@selected='selected']";

            LoginPage UserLogin = new LoginPage(driver);
            UserLogin.Login(Settings.AdminName, Settings.AdminPassword, true);

            driver.FindElement(By.XPath(GeoZonesMenuXpath)).Click();

            IList<IWebElement> geoZones = driver.FindElements(By.XPath(GeoZoneXpath));

            for (int i = 0; i < geoZones.Count; i++)
            {
                geoZones[i].FindElement(By.XPath(GeoZoneNameXpath)).Click();
                wait.Until(ExpectedConditions.ElementExists(By.XPath(ZoneXpath)));

                IList<IWebElement> geoZoneNames = driver.FindElements(By.XPath(ZoneXpath));

                bool isSortingByAsc = true;

                for (int j = 0; j < geoZoneNames.Count - 1; j++)
                {
                    if (geoZoneNames[j].GetAttribute("outerText").CompareTo(geoZoneNames[j + 1].GetAttribute("outerText")) == 1)
                    {
                        isSortingByAsc = false;
                        break;
                    }
                }
                Assert.IsTrue(isSortingByAsc);

                driver.FindElement(By.XPath(GeoZonesMenuXpath)).Click();
                geoZones = driver.FindElements(By.XPath(GeoZoneXpath));
            }
        }
    }
}
