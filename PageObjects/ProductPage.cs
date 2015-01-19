using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace PageObjects
{
    public class ProductPage
    {
        private IWebDriver _driver;
        IWebElement _addToBasketBtn;

        private const string AddToBucketBtnId = "//*[@id='sma-offer-buy']/div/button[1]";
        private const string AddToBucketBtnAlternate = "//*[@id='add-to-cart-btn']";

        public ProductPage(IWebDriver driver)
        {
            _driver = driver;
            InitializeAddToBucketBtn();
        }

        private void InitializeAddToBucketBtn()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            try
            {
                _addToBasketBtn = wait.Until(d => d.FindElement(By.XPath(AddToBucketBtnId)));
            }
            catch (Exception)
            {
                _addToBasketBtn = wait.Until(d => d.FindElement(By.XPath(AddToBucketBtnAlternate)));
            }
        }

        public BasketPage AddToBasket()
        {
            _addToBasketBtn.Click();
            return new BasketPage(_driver);
        }
    }
}