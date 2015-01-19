using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using PageObjects;

namespace SeleniumWebTests
{
    [TestFixture]
    public class SeleniumTest
    {
        private IWebDriver _driver;
        private HomePage _homePage;

        private const string VisibleForm = "//*[@class='main-wrapper responsive-content slide-out-navigation']";

        private const string UserLogin = "tgt.tests@gmail.com";
        private const string UserPassword = "TGT_Allegro_2014";

        private const string SearchTxt = "laptop";
        private const string SearchFieldElementId = "main-search-text";
        private const string SearchBtnElementPath = "//input[@class='sprite search-btn']";
        private const string SearchResultListId = "featured-offers";
        private const string AddToBucketBtnId = "//*[@id='sma-offer-buy']/div/button[1]";
        private const string AddToBucketBtnAlternate = "//*[@id='add-to-cart-btn']";
        private const string BucketModuleId = "cartModule";
        private const string ItemFromSearchResults = "//*[@id='featured-offers']/article[{0}]//h2//span";
        private const string ItemFromTitleFromBucketPath = "//a[@class='title']";

        [SetUp]
        public void OpenConnection()
        {
            _driver = new ChromeDriver();
            _homePage = HomePage.GoTo(_driver);
        }

        [TearDown]
        public void CloseConnection()
        {
            _driver.Quit();
        }

        [Test]
        public void LoginTest()
        {
            Assert.IsTrue(_homePage.IsLoaded());
            var loginPage = _homePage.GoToLoginPage();
            Assert.IsTrue(loginPage.IsLoaded());

            var userPage = loginPage.LoginAs(UserLogin, UserPassword);
            Assert.AreEqual(UserLogin, userPage.GetUserName());

            var homePage = userPage.Logout();
            Assert.IsTrue(_homePage.IsLoaded());
            Assert.IsFalse(homePage.IsUserLoggedIn());
        }

        [Test]
        public void SearchTest()
        {
            Assert.IsTrue(_homePage.IsLoaded());

            // tyoe search text and submit
            _driver.FindElement(By.Id(SearchFieldElementId)).SendKeys(SearchTxt);
            _driver.FindElement(By.XPath(SearchBtnElementPath)).Click();

            // check search results exist
            Assert.IsTrue(_driver.FindElement(By.Id(SearchResultListId)).Displayed);

            // you expect name of the 1st item to contain search text
            var itemTitle = _driver.FindElement(By.XPath(string.Format(ItemFromSearchResults, 1))).Text;
            Assert.IsTrue(itemTitle.ToLower().Contains(SearchTxt));

            // click 1st result
            _driver.FindElement(By.XPath(string.Format(ItemFromSearchResults, 1))).Click();

            // add to bucket
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement addToBucketBtn;

            try
            {
                addToBucketBtn = wait.Until(d => d.FindElement(By.XPath(AddToBucketBtnId)));
            }
            catch (Exception)
            {
                addToBucketBtn = wait.Until(d => d.FindElement(By.XPath(AddToBucketBtnAlternate)));
            }

            Assert.IsTrue(addToBucketBtn.Displayed);
            addToBucketBtn.Click();

            // check bucket is displayed
            wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var basket = wait.Until(d => d.FindElement(By.Id(BucketModuleId)));
            Assert.IsTrue(basket.Displayed);

            // check the name of product matches
            Assert.AreEqual(
                basket.FindElement(By.XPath(ItemFromTitleFromBucketPath)).Text,
                itemTitle);
        }
    }
}
