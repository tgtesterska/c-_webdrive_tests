using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace PageObjects
{
    public class BasketPage
    {
        private IWebDriver _driver;
        private const string BucketModuleId = "cartModule";
        private const string ItemFromTitleFromBucketPath = "//a[@class='title']";

        public BasketPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement BasketContainer
        {
            get
            {
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
                return wait.Until(d => d.FindElement(By.Id(BucketModuleId)));
            }
        }

        public bool ContainsProductWithName(string productName)
        {
            return BasketContainer.FindElement(By.XPath(ItemFromTitleFromBucketPath)).Text.Equals(productName);
        }
    }
}