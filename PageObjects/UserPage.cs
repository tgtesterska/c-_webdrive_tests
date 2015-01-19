using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace PageObjects
{
    public class UserPage
    {
        private IWebDriver _driver;

        private const string VisibleForm = "//*[@class='main-wrapper responsive-content slide-out-navigation']";
        private const string UserLoginElementPath = VisibleForm + "//*[@id='user-login']//span";
        private const string LogoutLinkPath = VisibleForm + "//*[@class='logout']//span";

        public UserPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public HomePage Logout()
        {
            _driver.FindElement(By.XPath(LogoutLinkPath)).Click();
            return new HomePage(_driver);
        }

        public string GetUserName()
        {
            return _driver.FindElement(By.XPath(UserLoginElementPath)).Text;
        }
    }
}
