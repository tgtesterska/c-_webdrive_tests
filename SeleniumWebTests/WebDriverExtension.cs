using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumWebTests
{
    public static class WebDriverExtension
    {
        public static IWebElement FindElement(this IWebDriver driver, By by, int timeout)
        {
            if (timeout > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(drv => drv.FindElement(by));
            }
            return driver.FindElement(by);
        }
    }
}