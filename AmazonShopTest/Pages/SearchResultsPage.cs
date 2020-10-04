using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonShopTest.Pages
{
    class SearchResultsPage
    {
        private IWebElement _firstArticle;
        private IWebElement _firstArticleLinkText;

        public string ArticleTitle { get; }
        public IWebDriver Driver { get; }

        public SearchResultsPage(IWebDriver driver)
        {
            Driver = driver;
            // wait for pageready
            var waitForSearchResults = new WebDriverWait(Driver, new TimeSpan(0, 0, 5));
            // first article after advertising has index 2
            _firstArticle = waitForSearchResults.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("[data-cel-widget='search_result_2']")));

            _firstArticleLinkText = _firstArticle.FindElement(By.CssSelector("h2 a"));
            ArticleTitle = _firstArticleLinkText.Text;
        }

        public bool HasResults(int count)
        {
            // count+1, because eg. 5th item has index 6 at amazon search-results
            string selector = "[data-component-type='s-search-result'][data-cel-widget='search_result_" + (count+1) + "']";
            try
            {
                // search_result_6 is the 5th Shoe found
                return Driver.FindElement(By.CssSelector(selector)).Size.Height > 0;
            }
            catch (NoSuchElementException)
            {

                return false;
            }
        }

        public void ClickFirstArticle()
        {
            _firstArticleLinkText.Click();
        }
    }
}
