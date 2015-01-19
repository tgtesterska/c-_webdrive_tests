using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace PageObjects
{
    public class LoginPage
    {
        private IWebDriver _driver;

        [FindsBy(How = How.Id, Using = "userForm_login")]
        [CacheLookup]
        private IWebElement UserName { get; set; }

        [FindsBy(How = How.Id, Using = "userForm_password")]
        [CacheLookup]
        private IWebElement Password { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@class='btn btn-primary']")]
        [CacheLookup]
        private IWebElement LoginButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='container authentication-container']")]
        [CacheLookup]
        private IWebElement AuthContainer { get; set; }

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }

        public UserPage LoginAs(string username, string password)
        {
            UserName.SendKeys(username);
            Password.SendKeys(password);
            LoginButton.Click();

            return new UserPage(_driver);
        }

        public bool IsLoaded()
        {
            return AuthContainer.Displayed;
        }
    }
}
