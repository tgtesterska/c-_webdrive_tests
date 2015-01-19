using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

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
        private const string ItemElementId = "pagecontent1";
        private const string AddToBucketBtnId = "add-to-cart-btn";
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
            _driver.FindElement(By.Id(SearchFieldElementId)).SendKeys(SearchTxt);
            _driver.FindElement(By.XPath(SearchBtnElementPath)).Click();
            Assert.IsTrue(_driver.FindElement(By.Id(SearchResultListId)).Displayed);
            var itemTitle = _driver.FindElement(By.XPath(string.Format(ItemFromSearchResults, 1))).Text;
            Assert.IsTrue(itemTitle.ToLower().Contains(SearchTxt));
            _driver.FindElement(By.XPath(string.Format(ItemFromSearchResults, 1))).Click();
            Assert.IsTrue(_driver.FindElement(By.Id(ItemElementId)).Displayed);
            _driver.FindElement(By.Id(AddToBucketBtnId)).Click();
            Assert.IsTrue(_driver.FindElement(By.Id(BucketModuleId)).Displayed);
            Assert.AreEqual(
                _driver.FindElement(By.Id(BucketModuleId)).FindElement(By.XPath(ItemFromTitleFromBucketPath)).Text,
                itemTitle);
        }
    }
}
