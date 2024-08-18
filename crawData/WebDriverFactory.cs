using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace crawData
{
    public interface IWebDriverFactory
    {
        IWebDriver CreateWebDriver();
    }

    internal class WebDriverFactory : IWebDriverFactory
    {
        public IWebDriver CreateWebDriver()
        {
            IWebDriver driver = new ChromeDriver();
            // driver.Manage().Window.Maximize();
            return driver;
        }
    }
}
