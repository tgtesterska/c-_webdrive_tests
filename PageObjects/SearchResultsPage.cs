using OpenQA.Selenium;

namespace PageObjects
{
    public class SearchResultsPage
    {
        private IWebDriver _driver;
        private const string SearchResultListId = "featured-offers";
        private const string ItemFromSearchResults = "//*[@id='featured-offers']/article[{0}]//h2//span";

        public SearchResultsPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public bool IsLoaded()
        {
            return _driver.FindElement(By.Id(SearchResultListId)).Displayed;
        }

        public string GetFirstResultName()
        {
            return _driver.FindElement(By.XPath(string.Format(ItemFromSearchResults, 1))).Text;
        }

        public ProductPage OpenFirstResult()
        {
            var firstResult = _driver.FindElement(By.XPath(string.Format(ItemFromSearchResults, 1)));
            firstResult.Click();

            return new ProductPage(_driver);
        }
    }
}