using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Tests.Features
{
    public class Driver
    {
        public static IWebDriver driver;

        public static IWebDriver GetDriver
        {
            get
            {
                if (driver == null)
                {
                    driver = new ChromeDriver();
                }

                return driver;
            }
        }
    }
}
