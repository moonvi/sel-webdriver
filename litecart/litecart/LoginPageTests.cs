using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;

namespace Litecart
{
    [TestFixture]
    public class LoginPageTests : TestBase
    {
        [Test]
        public void CheckIfLoginSuccesful()
        {          
            LoginPage UserLogin = new LoginPage(driver);

            UserLogin.Login(Settings.AdminName, Settings.AdminPassword, true);
        }
    }
}
