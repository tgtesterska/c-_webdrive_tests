using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace PageObjects
{
    public class LoginPage
    {
        private IWebDriver _driver;

        private const string InputLoginId = "userForm_login";
        private const string InputPasswordId = "userForm_password";
        private const string LoginBtnPath = "//button[@class='btn btn-primary']";
        private const string AuthContainerElementPath = "//*[@class='container authentication-container']";

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public UserPage LoginAs(string username, string password)
        {
            _driver.FindElement(By.Id(InputLoginId)).SendKeys(username);
            _driver.FindElement(By.Id(InputPasswordId)).SendKeys(password);
            _driver.FindElement(By.XPath(LoginBtnPath)).Click();

            return new UserPage(_driver);
        }

        public bool IsLoaded()
        {
            return _driver.FindElement(By.XPath(AuthContainerElementPath)).Displayed;
        }
    }
}
