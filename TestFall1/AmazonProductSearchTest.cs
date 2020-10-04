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
            var waitForCookyWindow = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
            var cookyWindowOkButton = waitForCookyWindow.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("sp-cc-accept")));
            cookyWindowOkButton.Click();

            //IsSearchBoxTest
            IWebElement searchBox = driver.FindElement(By.Id("twotabsearchtextbox"));

            // Assert

            //IsSearchBoxTextEqualTest
            searchBox.SendKeys("adidas schuhe");

            searchBox.SendKeys(Keys.Enter);

            // Assert

            // FindFirstArticleTest
            var waitForSearchResults = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
            IWebElement firstArticle = waitForSearchResults.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("[data-cel-widget='search_result_2']")));

            IWebElement firstArticleLinkText = firstArticle.FindElement(By.CssSelector("h2 a"));
            firstArticleLinkText.Click();

            // SelectSize
            var waitForArticlePage = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
            IWebElement dropdownMenuSelectSize = waitForArticlePage.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("native_dropdown_selected_size_name")));

            SelectElement selectSize = new SelectElement(dropdownMenuSelectSize);
            selectSize.SelectByIndex(1);

            // AddToCart
            IWebElement addToCartButton = driver.FindElement(By.Id("add-to-cart-button"));
            addToCartButton.Click();
        }
    }
}