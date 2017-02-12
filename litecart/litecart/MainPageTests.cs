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

        [Test]
        public void RegisterNewUser()
        {
            const string NewCustomerLinkText = "New customers click here";
            const string CreateAccountHeaderCssSelector = "h1.title";
            const string CreateAccountFormXpath = "//form[@name='customer_form']";
            const string CompanyFieldXpath = ".//td/input[@name='company']";
            const string FNameFieldXpath = ".//td/input[@name='firstname']";
            const string LNameFieldXpath = ".//td/input[@name='lastname']";
            const string Address1FieldXpath = ".//td/input[@name='address1']";
            const string PostcodeFieldXpath = ".//td/input[@name='postcode']";
            const string CityFieldXpath = ".//td/input[@name='city']";
            const string CountrySelectXpath = ".//td/span[contains(@class,'select2')]";
            const string CountrySearchXpath = "//input[@class='select2-search__field']";
            const string CountryNameXpath = "//span[contains(@class,'select2-results')]/ul/li[1]";
            const string ZoneSelectXpath = ".//td/select[@name='zone_code']";
            const string ZoneNameXpathFormat = ".//td/select[@name='zone_code']/option[@value='{0}']";
            const string EmailFieldXpath = ".//td/input[@name='email']";
            const string PhoneFieldXpath = ".//td/input[@name='phone']";
            const string PassXpath = ".//td/input[@name='password']";
            const string ConfirmPassXpath = ".//td/input[@name='confirmed_password']";
            const string SubmitButtonXpath = ".//button[@name='create_account']";
            const string LogoutTextLink = "Logout";

            //Login
            const string LoginHeaderCssSelector = "h3.title";
            const string LoginFormXpath = "//div[@id='box-account-login']";
            const string LoginEmailAddressXpath = ".//input[@name='email']";
            const string LoginPasswordXpath = ".//input[@name='password']";
            const string LoginButtonXpath = ".//button[@name='login']";

            //Test data
            const string Company = "Sherlock and Co.";
            const string FName = "Sherlock";
            const string LName = "Holmes";
            const string Address1 = "221B Baker Street";
            const string Postcode = "92626";
            const string City = "Costa Mesa";
            const string Country = "United States";
            const string PhoneNumber = "+1-541-754-3010";
            const string Pass = "Password@123";
            const string EmailFormatString = "user{0}@email.com";
            string UserEmail = string.Format(EmailFormatString, RandomGenerator.GetTimeBasedRndNum().ToString());

            MainPage HomePage = new MainPage(driver);
            HomePage.OpenMainPage();

            IWebElement createAccountLink = driver.FindElement(By.LinkText(NewCustomerLinkText));
            createAccountLink.Click();
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector(CreateAccountHeaderCssSelector)));

            IWebElement createAccountForm = driver.FindElement(By.XPath(CreateAccountFormXpath));

            createAccountForm.FindElement(By.XPath(CompanyFieldXpath)).SendKeys(Company);
            createAccountForm.FindElement(By.XPath(FNameFieldXpath)).SendKeys(FName);
            createAccountForm.FindElement(By.XPath(LNameFieldXpath)).SendKeys(LName);
            createAccountForm.FindElement(By.XPath(Address1FieldXpath)).SendKeys(Address1);
            createAccountForm.FindElement(By.XPath(PostcodeFieldXpath)).SendKeys(Postcode);
            createAccountForm.FindElement(By.XPath(CityFieldXpath)).SendKeys(City);

            FillAutoComplete(createAccountForm, By.XPath(CountrySelectXpath), 
                By.XPath(CountrySearchXpath), By.XPath(CountryNameXpath), Country);

            string californiaValueXpath = string.Format(ZoneNameXpathFormat,"CA"); //state = California
            SelectDropdownValue(createAccountForm, By.XPath(ZoneSelectXpath), By.XPath(californiaValueXpath));

            createAccountForm.FindElement(By.XPath(EmailFieldXpath))
                .SendKeys(UserEmail);
            createAccountForm.FindElement(By.XPath(PhoneFieldXpath)).SendKeys(PhoneNumber);
            createAccountForm.FindElement(By.XPath(PassXpath)).SendKeys(Pass);
            createAccountForm.FindElement(By.XPath(ConfirmPassXpath)).SendKeys(Pass);
            createAccountForm.FindElement(By.XPath(SubmitButtonXpath)).Click();
            wait.Until(ExpectedConditions.ElementExists(By.LinkText(LogoutTextLink)));

            IWebElement userLogout = driver.FindElement(By.LinkText(LogoutTextLink));
            userLogout.Click();
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector(LoginHeaderCssSelector)));

            IWebElement loginForm = driver.FindElement(By.XPath(LoginFormXpath));
            loginForm.FindElement(By.XPath(LoginEmailAddressXpath)).SendKeys(UserEmail);
            loginForm.FindElement(By.XPath(LoginPasswordXpath)).SendKeys(Pass);
            loginForm.FindElement(By.XPath(LoginButtonXpath)).Click();
            wait.Until(ExpectedConditions.ElementExists(By.LinkText(LogoutTextLink)));

            IWebElement secondLogout = driver.FindElement(By.LinkText(LogoutTextLink));
            secondLogout.Click();
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector(LoginHeaderCssSelector)));
        }
    }
}
