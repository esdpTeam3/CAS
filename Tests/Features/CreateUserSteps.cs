using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace Tests.Features
{
    [Binding]
    public class CreateUserSteps
    {
        private IWebDriver driver;

        public CreateUserSteps()
        {
            driver = Driver.GetDriver;
        }

        [When(@"Я нажимаю на ссылку ""(.*)""")]
        public void WhenЯНажимаюНаСсылку(string p0)
        {
            driver.FindElement(By.XPath("/html/body/main/div/div[1]/div/div/ul[1]/li[2]/a")).Click();
        }

        [When(@"Нажимаю на ссылку ""(.*)""")]
        public void WhenНажимаюНаСсылку(string p0)
        {
            driver.FindElement(By.XPath("/html/body/main/div/div[2]/div/div[1]/div[1]/a/img")).Click();
        }

        [Then(@"Вижу Страницу со списком пользователей")]
        public void ThenВижуСтраницуСоСпискомПользователей()
        {
            driver.FindElement(By.XPath("/html/body/main/div/div[2]/div/h2"));
        }

        [Then(@"Вижу страницу создания пользователя")]
        public void ThenВижуСтраницуСозданияПользователя()
        {
            driver.FindElement(By.XPath("/html/body/main/div/div[2]/h2"));
        }

        [Then(@"Ввожу данные")]
        public void ThenВвожуДанные()
        {
            driver.FindElement(By.Name("Login")).SendKeys("Conor");
            driver.FindElement(By.Name("FullName")).SendKeys("Conor");
            driver.FindElement(By.Name("Email")).SendKeys("conor@mac.com");
            driver.FindElement(By.Name("Password")).SendKeys("123456");
            driver.FindElement(By.Name("ConfirmPassword")).SendKeys("123456");
            driver.FindElement(By.Name("DatePassword")).SendKeys("24.10.2018");
            driver.FindElement(By.Name("PasswordNotifications")).SendKeys("1");
            driver.FindElement(By.Name("ContractNotifications")).SendKeys("1");
        }

        [Then(@"Нажимаю на кнопку ""(.*)""")]
        public void ThenНажимаюНаКнопку(string p0)
        {
            driver.FindElement(By.ClassName("btn")).Click();
        }

        [Then(@"Вижу главную страницу")]
        public void ThenВижуГлавнуюСтраницу()
        {
            driver.FindElement(By.XPath("/html/body/main/div/div[2]/h4")).Click();
        }
    }
}
