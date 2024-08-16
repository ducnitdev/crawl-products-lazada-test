using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
