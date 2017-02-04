using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litecart
{
    public class MainPage
    {
        const string HomeUrl= "http://localhost/litecart";

        private IWebDriver _driver;

        public MainPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void OpenMainPage()
        {
            _driver.Url = HomeUrl;
        }

    }
}
