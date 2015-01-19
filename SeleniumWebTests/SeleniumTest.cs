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

        private const string UserLogin = "tgt.tests@gmail.com";
        private const string UserPassword = "TGT_Allegro_2014";

        private const string SearchTxt = "laptop";

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

            var searchResultsPage = _homePage.Search(SearchTxt);
            Assert.IsTrue(searchResultsPage.IsLoaded());

            var firstResultName = searchResultsPage.GetFirstResultName();
            Assert.IsTrue(firstResultName.ToLower().Contains(SearchTxt));

            var productPage = searchResultsPage.OpenFirstResult();
            var basketPage = productPage.AddToBasket();
            Assert.IsTrue(basketPage.BasketContainer.Displayed);

            var containsProduct = basketPage.ContainsProductWithName(firstResultName);
            Assert.IsTrue(containsProduct);
        }
    }
}
