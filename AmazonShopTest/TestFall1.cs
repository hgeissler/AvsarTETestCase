using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using AmazonShopTest.Pages;

namespace AmazonShopTest
{
    [TestFixture]
    public class TestFall1
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void ShopAdidasShoeTest()
        {
            // Go to Amazon landing page
            var landingPage = new LandingPage(driver);

            // Assert
            Assert.IsTrue(landingPage.IsExpectedPageTitleStart, "The Title of this webpage does not start with \"Amazon.de:\"");
            Assert.IsTrue(landingPage.HasDesktopBanner, "The Hero Banner from Amazon landing page has not been loaded");

            landingPage.CloseCookiesPopup();

            // Search for "adidas schuhe" via search box
            landingPage.SearchForArticle("adidas schuhe");

            // wait for search results to load
            var searchResultsPage = new SearchResultsPage(driver);

            // Assert
            Assert.IsTrue(searchResultsPage.HasResults(5), "No search item found at 5th position");

            // Click on first article
            searchResultsPage.ClickFirstArticle();

            // Check if article page is loaded
            var articlePage = new ArticlePage(driver);

            // Assert
            Assert.IsTrue(articlePage.ArticleTitle.Contains(searchResultsPage.ArticleTitle), "The correct article page has not loaded. Article text from search does not match with title from article page");

            // Select article size
            //articlePage.SelectSize();

            // Add article to shopping cart
            articlePage.ClickAddToCart();

            // Wait for shopping cart page to load
            var shoppingCartPage = new ShoppingCartPage(driver);

            // Assert
            Assert.IsTrue(driver.Title.Equals("Amazon.de Einkaufswagen"), "The title for shopping cart page does not show");
            Assert.AreEqual("Zum Einkaufswagen hinzugefügt", shoppingCartPage.AddedToCartText, "The message \"Zum Einkaufswagen hizugefügt\" does not show");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}