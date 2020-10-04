using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

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
            //driver.Manage().Window.Maximize();
        }

        [Test]
        public void GoToAmazonTest()
        {
            driver.Navigate().GoToUrl(@amazonUrl);

            // Assert

            // Close Cooky Window
            //var waitForCookieWindow = new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(.ElementToBeClickable(By.XPath("//a/h3")));
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
            var cookyWindowOkButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("sp-cc-accept")));
            cookyWindowOkButton.Click();

            //IsSearchBoxTest
            IWebElement searchBox = driver.FindElement(By.Id("twotabsearchtextbox"));

            // Assert

            //IsSearchBoxTextEqualTest
            searchBox.SendKeys("adidas schuhe");

            searchBox.SendKeys(Keys.Enter);

            // Assert

            // FindFirstArticleTest
            //IWebElement searchResults = driver.FindElement(By.CssSelector("[data-component-type='s-search-results']"));
            //IWebElement firstArticle = driver.FindElement(By.CssSelector("[data-cel-widget='search_result_2']"));
            IWebElement firstArticle = driver.FindElement(By.CssSelector("[data-index='2']"));
            IWebElement firstArticleLinkText = firstArticle.FindElement(By.CssSelector("h2 a"));
            firstArticleLinkText.Click();

            // SelectSize
            SelectElement dropdownMenuSelectSize = new SelectElement(driver.FindElement(By.Id("native_dropdown_selected_size_name")));
            dropdownMenuSelectSize.SelectByIndex(1);
        }
    }
}