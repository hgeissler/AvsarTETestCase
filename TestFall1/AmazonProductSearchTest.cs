using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestFall1
{
    [TestFixture]
    public class AmazonProductSearchTest
    {
        string amazonUrl = "https://www.amazon.de";
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void GoToAmazonTest()
        {
            driver.Navigate().GoToUrl(@amazonUrl);

            // Assert

            //IsSearchBoxTest
            IWebElement searchBox = driver.FindElement(By.Id("twotabsearchtextbox"));

            // Assert

            //IsSearchBoxTextEqual
            searchBox.SendKeys("adidas schuhe");

            searchBox.SendKeys(Keys.Enter);

            // assert
        }
    }
}