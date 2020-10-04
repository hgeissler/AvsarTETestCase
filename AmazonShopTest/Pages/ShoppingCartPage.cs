using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonShopTest.Pages
{
    class ShoppingCartPage
    {
        public string AddedToCartText { get; }
        public IWebDriver Driver { get; }

        public ShoppingCartPage(IWebDriver driver)
        {
            Driver = driver;
            // wait for pageready
            var waitForShoppingCart = new WebDriverWait(Driver, new TimeSpan(0, 0, 10));
            IWebElement addedToCart = waitForShoppingCart.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("[id='huc-v2-order-row-messages']")));

            AddedToCartText = addedToCart.Text;
        }

        
    }
}
