using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace litecart
{
    public class LoginPage
    {
        const string LoginUrl = "http://localhost/litecart/admin/login.php";

        private IWebDriver _driver;
        
        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Login(string username, string password, bool remember)
        {
            _driver.Url = LoginUrl;

            IWebElement usernameField = _driver.FindElement(By.Name("username"));
            usernameField.SendKeys(username);

            IWebElement passField = _driver.FindElement(By.Name("password"));
            passField.SendKeys(password);

            if(remember)
            {
                IWebElement rememberMeCheckbox = _driver.FindElement(By.Name("remember_me"));
                rememberMeCheckbox.Click();
            }

            IWebElement loginBtn = _driver.FindElement(By.Name("login"));
            loginBtn.Click();

        }

    }
}
