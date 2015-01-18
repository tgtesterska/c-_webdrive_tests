using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace SeleniumWebTests
{
    [TestFixture]
    class SeleniumTest
    {
        private IWebDriver _driver;
        private const string Url = "http://www.allegro.pl";
        private const string Url2 = "http://allegro.pl/";
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
        private const string ItemElementId = "pagecontent1";
        private const string AddToBucketBtnId = "add-to-cart-btn";
        private const string BucketModuleId = "cartModule";
        private const string ItemFromSearchResults = "//*[@id='featured-offers']/article[{0}]//h2//span";
        private const string ItemFromTitleFromBucketPath = "//a[@class='title']";
        private int _waitSeconds = 5;

        [SetUp]
        public void OpenInitPage()
        {
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl(Url);
        }

        [TearDown]
        public void CloseDriver()
        {
            _driver.Close();
        }

        [Test]
        public void LoginTestChrome()
        {
            Login("chrome");
        }

        [Test]
        public void SearchTestChrome()
        {
            Search();
        }

        private void CloseConnection()
        {
////            _driver.Quit();
        }

        public void IsOnHomePage()
        {
            Assert.IsTrue(_driver.FindElement(By.ClassName(MainBoxClass), _waitSeconds).Displayed);
        }

        public void Search()
        {
            try
            {
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(_waitSeconds));
                IsOnHomePage();
                _driver.FindElement(By.Id(SearchFieldElementId), _waitSeconds).SendKeys(SearchTxt);
                _driver.FindElement(By.XPath(SearchBtnElementPath), _waitSeconds).Click();
                Assert.IsTrue(_driver.FindElement(By.Id(SearchResultListId), _waitSeconds).Displayed);
                var itemTitle =
                    _driver.FindElement(By.XPath(string.Format(ItemFromSearchResults, 1)), _waitSeconds).Text;
                Assert.IsTrue(itemTitle.ToLower().Contains(SearchTxt));
                _driver.FindElement(By.XPath(string.Format(ItemFromSearchResults, 1)), _waitSeconds).Click();
                Assert.IsTrue(_driver.FindElement(By.Id(ItemElementId), _waitSeconds).Displayed);
                _driver.FindElement(By.Id(AddToBucketBtnId), _waitSeconds).Click();
                IWebElement elem0;
                do
                {
                    elem0 = _driver.FindElement(By.Id(BucketModuleId), _waitSeconds);
                }
                while (elem0.Displayed == false);

                Assert.IsTrue(elem0.Displayed);
                var elem = _driver.FindElement(By.Id(BucketModuleId), _waitSeconds);
                var elem2 = wait.Until(x => elem.FindElement(By.XPath(ItemFromTitleFromBucketPath)));
                var s = elem2.Text;
                Assert.AreEqual(s, itemTitle);
            }
            finally
            {
                CloseConnection();
            }
        }

        public void Login(string kind = null)
        {
            try
            {
                IsOnHomePage();
                _driver.FindElement(By.XPath(LoginLinkPath), _waitSeconds).Click();
                Assert.IsTrue(_driver.FindElement(By.XPath(AuthContainerElementPath), _waitSeconds).Displayed);
                if (kind == "chrome")
                {
                    do
                    {
                        Log();
                    }
                    while (_driver.Url != Url2);
                }
                else
                {
                    Log();
                }

                IsOnHomePage();
                Assert.AreEqual(_driver.FindElement(By.XPath(UserLoginElementPath), _waitSeconds).Text, UserLogin);
                _driver.FindElement(By.XPath(LogoutLinkPath), _waitSeconds).Click();
                IsOnHomePage();
                Assert.IsTrue(_driver.FindElement(By.XPath(LoginLinkPath), _waitSeconds).Displayed);
            }
            finally
            {
                CloseConnection();
            }
        }

        public void Log()
        {
            _driver.FindElement(By.Id(InputLoginId), _waitSeconds).SendKeys(UserLogin);
            _driver.FindElement(By.Id(InputPasswordId), _waitSeconds).SendKeys(UserPassword);
            _driver.FindElement(By.XPath(LoginBtnPath), _waitSeconds).Click();
        }
    }
}
