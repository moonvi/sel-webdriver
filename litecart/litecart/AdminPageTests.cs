using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Litecart
{
    [TestFixture]
    public class AdminPageTests : TestBase
    {
        private const string HeaderCssSelector = "td#content h1";
        private const string CountriesMenuXpath = "//ul[@id='box-apps-menu']/li[3]";

        [Test]
        public void CheckMenuHeadersPresented()
        {
            const string InitialMainMenuItemXpath = "//ul[@id='box-apps-menu']/li";
            const string NextMainMenuItemXpath = "//ul[@id='box-apps-menu']/li[@class='selected']/following-sibling::li[1]";
            const string NextSubMenuItemXpath = "//ul[@class='docs']/li[@class='selected']/following-sibling::li[1]";

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
            const string CountriesXpath = "//table[@class='dataTable']/tbody/tr[@class='row']/td[5]";

            LoginPage UserLogin = new LoginPage(driver);
            UserLogin.Login(Settings.AdminName, Settings.AdminPassword, true);

            IWebElement countriesMenu = driver.FindElement(By.XPath(CountriesMenuXpath));
            countriesMenu.Click();

            IList<IWebElement> countries = driver.FindElements(By.XPath(CountriesXpath));

            List<string> countriesNames = new List<string>();

            for (int i = 0; i < countries.Count; i++)
            {
                countriesNames.Add(countries[i].Text);
            }

            bool isSortingByAsc = true;

            for (int i = 0; i < countriesNames.Count - 1; i++)
            {
                if (countriesNames[i].CompareTo(countriesNames[i + 1]) == 1)
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

            for (int i = 0; i < zones.Count; i++)
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

        [Test]
        public void AddingNewProduct()
        {
            const string CatalogMenuXpath = "//ul[@id='box-apps-menu']/li[2]";
            const string CatalogMenuSelectedXpath = "//ul[@class='docs']/li[@class='selected']";
            const string AddNewProdButtonXpath = "//a[@class='button'][2]";
            const string SaveButtonXpath = "//button[@type='submit'][@name='save']";
            const string productNameXpath = "//table[@class='dataTable']/tbody/tr[@class='row']/td[3]";

            //General tab Xpath
            const string GeneralTabXpath = "//div[@class='tabs']/ul/li[1]";
            const string GeneralContentXpath = "//div[@class='content']";
            const string ProductEnableXpath = GeneralContentXpath + "//input[@type='radio'][@value='1']";
            const string ProdNameXpath = GeneralContentXpath + "//input[@name='name[en]']";
            const string ProdCodeXpath = GeneralContentXpath + "//input[@name='code']";
            const string CategoryXpath = GeneralContentXpath + "//input[@type='checkbox'][@name='categories[]'][@value='1']";
            const string DefaultCategorySelectXpath = GeneralContentXpath + "//select[@name='default_category_id']";
            const string DefaultCategoryXpathFormat = GeneralContentXpath + "//select[@name='default_category_id']/option[@value='{0}']";
            const string GenderXpath = GeneralContentXpath + "//input[@name='product_groups[]'][@value='1-2']";
            const string QuantityXpath = GeneralContentXpath + "//input[@name='quantity']";
            const string UploadImageXpath = GeneralContentXpath + "//input[@type='file']";

            //Information tab Xpath
            const string InfoTabXpath = "//div[@class='tabs']/ul/li[2]";
            const string InfoContentXpath = "//div[@class='content']";
            const string ManufacturerSelectPath = InfoContentXpath + "//select[@name='manufacturer_id']";
            const string ManufacturerXpathFormat = InfoContentXpath + "//select[@name='manufacturer_id']/option[@value='{0}']";
            const string KeywordsXpath = InfoContentXpath + "//input[@name='keywords']";
            const string ShortDescriptionXpath = InfoContentXpath + "//input[@name='short_description[en]']";
            const string DescriptionXpath = InfoContentXpath + "//div[@class='trumbowyg-editor']";
            const string HeadTitleXpath = InfoContentXpath + "//input[@name='head_title[en]']";
            const string MetaDescriptionXpath = InfoContentXpath + "//input[@name='meta_description[en]']";

            //Prices tab Xpath
            const string PricesTabXpath = "//div[@class='tabs']/ul/li[4]";
            const string PricesContentXpath = "//div[@class='content']";
            const string PurchasePriceXpath = PricesContentXpath + "//input[@name='purchase_price']";
            const string CurrencyCodeSelectXpath = PricesContentXpath + "//select[@name='purchase_price_currency_code']";
            const string CurrencyCodeXpathFormat = PricesContentXpath + "//select[@name='purchase_price_currency_code']/option[@value='{0}']";
            const string PriceUSDXpath = PricesContentXpath + "//input[@name='prices[USD]']";
            const string PriceEURXpath = PricesContentXpath + "//input[@name='prices[EUR]']";

            //Test data
            const string ProductNameFormatString = "Test product {0}";
            string ProductName = string.Format(ProductNameFormatString, RandomGenerator.GetTimeBasedRndNum().ToString());
            const string ProductCode = "test001";
            const string Quantity = "10";
            const string Keywords = "new, test";
            const string ShortDescription = "New test product";
            const string Description = "Full product description for a new product";
            const string HeadTitle = "Test title";
            const string MetaDescription = "Test meta description";
            const string PurchasePrice = "15.50";
            const string PriceUSD = "25";
            const string PriceEUR = "28";

            LoginPage UserLogin = new LoginPage(driver);
            UserLogin.Login(Settings.AdminName, Settings.AdminPassword, true);

            IWebElement catalogMenu = driver.FindElement(By.XPath(CatalogMenuXpath));
            catalogMenu.Click();
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector(HeaderCssSelector)));

            IWebElement addNewProdButton = driver.FindElement(By.XPath(AddNewProdButtonXpath));
            addNewProdButton.Click();
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector(HeaderCssSelector)));

            //General tab
            IWebElement generalTab = driver.FindElement(By.XPath(GeneralTabXpath));
            generalTab.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(ProductEnableXpath)));

            driver.FindElement(By.XPath(ProductEnableXpath)).Click();
            driver.FindElement(By.XPath(ProdNameXpath)).SendKeys(ProductName);
            driver.FindElement(By.XPath(ProdCodeXpath)).SendKeys(ProductCode);
            driver.FindElement(By.XPath(CategoryXpath)).Click();

            string defaultCategory = string.Format(DefaultCategoryXpathFormat, "1");
            SelectDropdownValue(driver, By.XPath(DefaultCategorySelectXpath), By.XPath(defaultCategory));

            driver.FindElement(By.XPath(GenderXpath)).Click();
            driver.FindElement(By.XPath(QuantityXpath)).Clear();
            driver.FindElement(By.XPath(QuantityXpath)).SendKeys(Quantity);

            // define path directory for product image
            string testBasedir = AppDomain.CurrentDomain.BaseDirectory;
            string imagePath = Path.Combine(testBasedir, "Images", "test001.jpg");

            driver.FindElement(By.XPath(UploadImageXpath)).SendKeys(imagePath);

            //Information tab
            IWebElement informationTab = driver.FindElement(By.XPath(InfoTabXpath));
            informationTab.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(KeywordsXpath)));

            string manufacturer = string.Format(ManufacturerXpathFormat, "1");
            SelectDropdownValue(driver, By.XPath(ManufacturerSelectPath), By.XPath(manufacturer));

            driver.FindElement(By.XPath(KeywordsXpath)).SendKeys(Keywords);
            driver.FindElement(By.XPath(ShortDescriptionXpath)).SendKeys(ShortDescription);
            driver.FindElement(By.XPath(DescriptionXpath)).SendKeys(Description);
            driver.FindElement(By.XPath(HeadTitleXpath)).SendKeys(HeadTitle);
            driver.FindElement(By.XPath(MetaDescriptionXpath)).SendKeys(MetaDescription);

            //Prices tab
            IWebElement pricesTab = driver.FindElement(By.XPath(PricesTabXpath));
            pricesTab.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(PurchasePriceXpath)));

            driver.FindElement(By.XPath(PurchasePriceXpath)).Clear();
            driver.FindElement(By.XPath(PurchasePriceXpath)).SendKeys(PurchasePrice);

            string currencyCode = string.Format(CurrencyCodeXpathFormat, "USD");
            SelectDropdownValue(driver, By.XPath(CurrencyCodeSelectXpath), By.XPath(currencyCode));

            driver.FindElement(By.XPath(PriceUSDXpath)).SendKeys(PriceUSD);
            driver.FindElement(By.XPath(PriceEURXpath)).SendKeys(PriceEUR);

            IWebElement saveProduct = driver.FindElement(By.XPath(SaveButtonXpath));
            saveProduct.Click();

            //Check that product is created
            IWebElement catalogMenuWithProducts = driver.FindElement(By.XPath(CatalogMenuSelectedXpath));
            catalogMenuWithProducts.Click();
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector(HeaderCssSelector)));

            IList<IWebElement> products = driver.FindElements(By.XPath(productNameXpath));

            List<string> productNames = new List<string>();

            for (int i = 0; i < products.Count; i++)
            {
                productNames.Add(products[i].Text);
            }

            Assert.Contains(ProductName, productNames);

        }

        [Test]
        public void CheckLinksOpenedInNewWindow()
        {
            const string EditCountryXpath = "//table[@class='dataTable']/tbody/tr[@class='row']/td[7]";
            const string EditLinkXpath = "./a";
            const string EditCountryHeaderXpath = "//h1[contains(text(),'Edit Country')]";
            const string ExternalLinksXpath = "//form/table//a[contains(@target,'_blank')]";

            LoginPage UserLogin = new LoginPage(driver);
            UserLogin.Login(Settings.AdminName, Settings.AdminPassword, true);

            IWebElement countriesMenu = driver.FindElement(By.XPath(CountriesMenuXpath));
            countriesMenu.Click();

            IWebElement editCountryIcon = driver.FindElement(By.XPath(EditCountryXpath));
            editCountryIcon.FindElement(By.XPath(EditLinkXpath)).Click();
            wait.Until(ExpectedConditions.ElementExists(By.XPath(EditCountryHeaderXpath)));

            IList<IWebElement> externalLinks = driver.FindElements(By.XPath(ExternalLinksXpath));

            for (int j = 0; j < externalLinks.Count; j++)
            {
                string mainWindow = driver.CurrentWindowHandle;
                IReadOnlyList<string> oldWindows = driver.WindowHandles;

                externalLinks[j].Click();
                string newWindow = wait.Until((d) => ThereIsWindowOtherThan(oldWindows));
                driver.SwitchTo().Window(newWindow);
                driver.Close();
                driver.SwitchTo().Window(mainWindow);
            }
            driver.FindElement(By.XPath(CountriesMenuXpath)).Click();
            //editCountryIcon = driver.FindElement(By.XPath(EditCountryXpath));

        }
    }
}
