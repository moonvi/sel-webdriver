using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using NUnit.Framework;

namespace SeleniumTest
{
    [TestFixture]
    public class TestInCoud
    {
        private IWebDriver driver;
        private DesiredCapabilities capability = DesiredCapabilities.Chrome();

        [SetUp]
        public void start()
        {
            capability.SetCapability("browserstack.user", "vikabudzko1");
            capability.SetCapability("browserstack.key", "VUpuufPxCiT3G5HzxMry");
            capability.SetCapability("build", "First build");
            capability.SetCapability("browserstack.debug", "true");
            capability.SetCapability("platform","WIN8");
            driver = new RemoteWebDriver(
             new Uri("http://hub-cloud.browserstack.com/wd/hub/"), capability
           );
        }

        [Test]
        public void CoudTest()
        {
            driver.Navigate().GoToUrl("http://www.google.com");
            Console.WriteLine(driver.Title);

            IWebElement query = driver.FindElement(By.Name("q"));
            query.SendKeys("Browserstack");
            query.Submit();
            Console.WriteLine(driver.Title);
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
        }
    }
}