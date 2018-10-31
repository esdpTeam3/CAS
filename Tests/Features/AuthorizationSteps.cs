using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using TechTalk.SpecFlow;

namespace Tests.Features
{
    [Binding]
    public class AuthorizationSteps
    {
        private IWebDriver driver;

        public AuthorizationSteps()
        {
            driver = Driver.GetDriver;
        }

        [Given(@"Я на странице авторизации")]
        public void ДопустимЯНаСтраницеАвторизации()
        {
            driver.Navigate().GoToUrl("http://localhost:55121");
        }
        
        [Given(@"Я анонимный пользователь")]
        public void ДопустимЯАнонимныйПользователь()
        {
            
        }
        
        [When(@"Я ввожу данные и нажимаю кнопку ""(.*)""")]
        public void ЕслиЯВвожуДанныеИНажимаюКнопку(string p0)
        {
            driver.FindElement(By.Name("Login")).SendKeys("Admin");
            driver.FindElement(By.Name("Password")).SendKeys("123456");
            driver.FindElement(By.ClassName("btn")).Click();
        }
        
        [Then(@"Я авторизуюсь на сайте")]
        public void ТоЯАвторизуюсьНаСайте()
        {
            string url = driver.Url;
            Assert.True(url == "http://localhost:55121/User");
        }
    }
}
