using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using AmazonShopTest.Pages;

namespace AmazonShopTest
{
    [TestFixture]
    public class TestFall2
    {
        IWebDriver driver;
        LandingPage landingPage;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void ShopAdidasShoeTest()
        {
            // Go to Amazon landing page
            landingPage = new LandingPage(driver);

            // Assert
            Assert.IsTrue(landingPage.IsExpectedPageTitleStart, "The Title of this webpage does not start with \"Amazon.de:\"");
            Assert.IsTrue(landingPage.HasDesktopBanner, "The Hero Banner from Amazon landing page has not been loaded");

            landingPage.CloseCookiesPopup();

            AddToCart("Adidas Herren Questar Flow Laufschuhe");
            AddToCart("Puma tazon 6");
            AddToCart("Nike Air Max");

            IWebElement cartCount = driver.FindElement(By.Id("nav-cart-count"));

            // Assert
            Assert.AreEqual("3", cartCount.Text, "The number of items in Shopping cart is not 3");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        public void AddToCart(string articleName)
        {
            // Search for "adidas schuhe" via search box
            landingPage.SearchForArticle(articleName);

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
            articlePage.SelectSize();

            // Add article to shopping cart
            articlePage.ClickAddToCart();

            // Wait for shopping cart page to load
            var shoppingCartPage = new ShoppingCartPage(driver);

            // Assert
            Assert.IsTrue(driver.Title.Equals("Amazon.de Einkaufswagen"), "The title for shopping cart page does not show");
            Assert.AreEqual("Zum Einkaufswagen hinzugefügt", shoppingCartPage.AddedToCartText, "The message \"Zum Einkaufswagen hizugefügt\" does not show");
        }
    }
}