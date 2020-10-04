using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestFall1.Pages
{
    public class LandingPage
    {
        private string _amazonUrl = "https://www.amazon.de";

        public IWebDriver Driver { get; }
        public bool IsExpectedPageTitleStart
        {
            get
            {
                return this.Driver.Title.StartsWith("Amazon.de:");
            }
        }
        public bool HasDesktopBanner
        {
            get
            {
                try
                {
                    return Driver.FindElement(By.Id("desktop-banner")).Size.Height > 0;
                }
                catch (NoSuchElementException)
                {

                    return false;
                }
            }
        }

        public LandingPage(IWebDriver driver)
        {
            Driver = driver;
            Driver.Navigate().GoToUrl(@_amazonUrl);
        }

        public void CloseCookiesPopup()
        {
            var waitForCookyWindow = new WebDriverWait(Driver, new TimeSpan(0, 0, 10));
            var cookyWindowOkButton = waitForCookyWindow.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("sp-cc-accept")));
            cookyWindowOkButton.Click();
        }

        public void SearchForArticle(string articleName)
        {
            IWebElement searchBox = Driver.FindElement(By.Id("twotabsearchtextbox"));
            searchBox.SendKeys(articleName);
            searchBox.SendKeys(Keys.Enter);
        }
    }
}
