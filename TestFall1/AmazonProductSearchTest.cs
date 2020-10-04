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
                hasDesktopBanner = driver.FindElement(By.Id("desktop-banner")).Size.Height > 0;
            }
            catch (NoSuchElementException)
            {

                hasDesktopBanner = false;
            }
            Assert.IsTrue(expectedPageTitleStart, "The Title of this webpage does not start with \"Amazon.de:\"");
            Assert.IsTrue(hasDesktopBanner, "The Hero Banner from Amazon landing page has not been loaded");

            // Close Popup Cookies Window

            var waitForCookyWindow = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
            var cookyWindowOkButton = waitForCookyWindow.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("sp-cc-accept")));
            cookyWindowOkButton.Click();

            // Search for "adidas schuhe" via search box
            IWebElement searchBox = driver.FindElement(By.Id("twotabsearchtextbox"));
            searchBox.SendKeys("adidas schuhe");
            searchBox.SendKeys(Keys.Enter);

            // wait for search results to load
            var waitForSearchResults = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
            IWebElement firstArticle = waitForSearchResults.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("[data-cel-widget='search_result_2']")));


            // Assert
            // Check if >= 5 shoes found.
            // search_result_6 is the 5th Shoe found
            bool hasFifthResult;
            try
            {
                hasFifthResult = driver.FindElement(By.CssSelector("[data-component-type='s-search-result'][data-cel-widget='search_result_6']")).Size.Height > 0;
            }
            catch (NoSuchElementException)
            {

                hasFifthResult = false;
            }
            Assert.IsTrue(hasFifthResult, "No search item found at 5th position");


            // Find first article and click it
            //IWebElement firstArticle = searchResults.FindElement(By.CssSelector("[data-cel-widget='search_result_2']"));
            IWebElement firstArticleLinkText = firstArticle.FindElement(By.CssSelector("h2 a"));

            string articleTitleFromSearchPage = firstArticleLinkText.Text;

            firstArticleLinkText.Click();

            // Check if article page is loaded

            var waitForArticlePage = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
            IWebElement dropdownMenuSelectSize = waitForArticlePage.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("addToCart")));

            // Assert
            string articleTitleFromArticlePage = driver.FindElement(By.Id("productTitle")).Text;
            Assert.IsTrue(articleTitleFromArticlePage.Contains(articleTitleFromSearchPage), "The correct article page has not loaded. Article text from search does not match with title from article page");
            //Assert.AreEqual(articleTitleFromSearchPage, articleTitleFromArticlePage, "The correct article page has not loaded. Article text from search does not match with title from article page");

            // Select article size

            try
            {
                IWebElement selectSizeElement = driver.FindElement(By.Id("native_dropdown_selected_size_name"));
                SelectElement selectSize = new SelectElement(dropdownMenuSelectSize);
                selectSize.SelectByIndex(1);
            }
            catch (NoSuchElementException)
            {

                System.Diagnostics.Debug.WriteLine("There is only one possible size");
            }


            // AddToCart
            IWebElement addToCartButton = driver.FindElement(By.Id("add-to-cart-button"));
            addToCartButton.Click();
        }
    }
}