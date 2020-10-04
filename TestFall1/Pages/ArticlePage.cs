using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestFall1.Pages
{
    class ArticlePage
    {
        public string ArticleTitle { get; }
        public IWebDriver Driver { get; }

        public ArticlePage(IWebDriver driver)
        {
            Driver = driver;
            // wait for pageready
            var waitForArticlePage = new WebDriverWait(Driver, new TimeSpan(0, 0, 5));
            waitForArticlePage.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("addToCart")));

            ArticleTitle = driver.FindElement(By.Id("productTitle")).Text;
        }

        public void SelectSize()
        {
            try
            {
                IWebElement selectSizeElement = Driver.FindElement(By.Id("native_dropdown_selected_size_name"));
                SelectElement selectSize = new SelectElement(selectSizeElement);
                selectSize.SelectByIndex(1);
            }
            catch (NoSuchElementException)
            {

                System.Diagnostics.Debug.WriteLine("There is only one possible size");
            }

        }

        public void ClickAddToCart()
        {
            IWebElement addToCartButton = Driver.FindElement(By.Id("add-to-cart-button"));
            addToCartButton.Click();
        }
    }
}
