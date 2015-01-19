using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumWebTests
{
    [TestFixture]
    public class SeleniumTest
    {
        private IWebDriver _driver;
        private const string Url = "http://www.allegro.pl";
        private const string MainBoxClass = "main-box";
        private const string VisibleForm = "//*[@class='main-wrapper responsive-content slide-out-navigation']";
        private const string LoginLinkPath = VisibleForm + "//*[@class='login']//span";
        private const string LogoutLinkPath = VisibleForm + "//*[@class='logout']//span";
        private const string AuthContainerElementPath = "//*[@class='container authentication-container']";
        private const string InputLoginId = "userForm_login";
        private const string InputPasswordId = "userForm_password";
        private const string LoginBtnPath = "//button[@class='btn btn-primary']";
        private const string UserLoginElementPath = VisibleForm + "//*[@id='user-login']//span";
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
            _driver.Navigate().GoToUrl(Url);
        }

        [TearDown]
        public void CloseConnection()
        {
            _driver.Quit();
        }

        public void IsOnHomePage()
        {
            Assert.IsTrue(_driver.FindElement(By.ClassName(MainBoxClass)).Displayed);
        }

        [Test]
        public void LoginTest()
        {
            IsOnHomePage();
            _driver.FindElement(By.XPath(LoginLinkPath)).Click();
            Assert.IsTrue(_driver.FindElement(By.XPath(AuthContainerElementPath)).Displayed);
            _driver.FindElement(By.Id(InputLoginId)).SendKeys(UserLogin);
            _driver.FindElement(By.Id(InputPasswordId)).SendKeys(UserPassword);
            _driver.FindElement(By.XPath(LoginBtnPath)).Click();
            IsOnHomePage();
            Assert.AreEqual(_driver.FindElement(By.XPath(UserLoginElementPath)).Text, UserLogin);
            _driver.FindElement(By.XPath(LogoutLinkPath)).Click();
            IsOnHomePage();
            Assert.IsTrue(_driver.FindElement(By.XPath(LoginLinkPath)).Displayed);
        }

        [Test]
        public void SearchTest()
        {
            IsOnHomePage();

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
