using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace PageObjects
{
    public class HomePage
    {
        private IWebDriver _driver;

        private const string Url = "http://www.allegro.pl";

        private const string MainBoxClass = "main-box";

        private const string VisibleForm = "//*[@class='main-wrapper responsive-content slide-out-navigation']";
        private const string LoginLinkPath = VisibleForm + "//*[@class='login']//span";
        private const string SearchFieldElementId = "main-search-text";
        private const string SearchBtnElementPath = "//input[@class='sprite search-btn']";

        public HomePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public bool IsLoaded()
        {
            return _driver.FindElement(By.ClassName(MainBoxClass)).Displayed;
        }

        public static HomePage GoTo(IWebDriver driver)
        {
            driver.Navigate().GoToUrl(Url);
            return new HomePage(driver);
        }

        public LoginPage GoToLoginPage()
        {
            _driver.FindElement(By.XPath(LoginLinkPath)).Click();
            return new LoginPage(_driver);
        }

        public bool IsUserLoggedIn()
        {
            return !_driver.FindElement(By.XPath(LoginLinkPath)).Displayed;
        }

        public SearchResultsPage Search(string searchTerm)
        {
            _driver.FindElement(By.Id(HomePage.SearchFieldElementId)).SendKeys(searchTerm);
            _driver.FindElement(By.XPath(HomePage.SearchBtnElementPath)).Click();

            return new SearchResultsPage(_driver);
        }
    }
}
