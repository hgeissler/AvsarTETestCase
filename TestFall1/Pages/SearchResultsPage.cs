using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestFall1.Pages
{
    class SearchResultsPage
    {
        private IWebElement _firstArticle;
        private IWebElement _firstArticleLinkText;

        public string ArticleTitle { get; }
        public IWebDriver Driver { get; }
        public bool HasFifthResult
        {
            get
            {
                try
                {
                    // search_result_6 is the 5th Shoe found
                    return Driver.FindElement(By.CssSelector("[data-component-type='s-search-result'][data-cel-widget='search_result_6']")).Size.Height > 0;
                }
                catch (NoSuchElementException)
                {

                    return false;
                }
            }
        }

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

        public void ClickFirstArticle()
        {
            _firstArticleLinkText.Click();
        }
    }
}
