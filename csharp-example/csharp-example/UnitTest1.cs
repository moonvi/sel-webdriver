using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace Task1
{
    [TestFixture]
    public class MyFirstTest
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        
        [SetUp]
        public void start()
        {
            //driver = new ChromeDriver();
            //driver = new RemoteWebDriver(new Uri("http://192.168.0.107:4444/wd/hub"), DesiredCapabilities.Chrome());
            driver = new RemoteWebDriver(new Uri("http://192.168.0.107:4444/wd/hub"), DesiredCapabilities.InternetExplorer());
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }
        
        [Test]
        public void FirstTest()
        {
            driver.Url = "http://selenium2.ru/";
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
