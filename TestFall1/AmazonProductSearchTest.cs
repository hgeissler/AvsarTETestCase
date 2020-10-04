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
        public void ShopAdidasShoeTest()
        {

            // Go to Amazon landing page

            driver.Navigate().GoToUrl(@amazonUrl);

            // Assert
            // Check if landing page is loaded correctly
            bool expectedPageTitleStart = driver.Title.StartsWith("Amazon.de:");
            bool hasDesktopBanner;
            try
            {
                hasDesktopBanner = driver.FindElement(By.Id("desktop-banna")).Size.Height > 0;
            }
            catch (NoSuchElementException)
            {

                hasDesktopBanner = false;
            }
            Assert.IsTrue(expectedPageTitleStart, "The Title of this webpage does not start with \"Amazon.de:\"");
            Assert.IsTrue(hasDesktopBanner, "The Hero Banner from Amazon landing page has not been loaded");

            // Close Cooky Window

            var waitForCookyWindow = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
            var cookyWindowOkButton = waitForCookyWindow.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("sp-cc-accept")));
            cookyWindowOkButton.Click();

            // Search for "adidas schuhe" via search box
            IWebElement searchBox = driver.FindElement(By.Id("twotabsearchtextbox"));
            searchBox.SendKeys("adidas schuhe");
            searchBox.SendKeys(Keys.Enter);

            // wait for search results to load
            var waitForSearchResults = new WebDriverWait(driver, new TimeSpan(0, 0, 5));           
            IWebElement searchResults = waitForSearchResults.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("[data-component-type='s-search-results']")));


            // Assert
            // Check if >= 5 shoes found.
            // search_result_6 is the 5th Shoe found
            bool hasFifthResult = driver.FindElement(By.CssSelector("[data-component-type='s-search-result'][data-cel-widget='search_result_6888']")).Size.Height > 0;
            Assert.IsTrue(hasFifthResult, "No search item found at 5th position");


            // FindFirstArticleTest
            IWebElement firstArticle = searchResults.FindElement(By.CssSelector("[data-cel-widget='search_result_2']"));
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