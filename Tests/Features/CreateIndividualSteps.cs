using System;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Tests.Features
{
    [Binding]
    public class CreateIndividualSteps
    {
        private IWebDriver driver;

        public CreateIndividualSteps()
        {
            driver = Driver.GetDriver;
        }

        [Given(@"Я нажимаю на ссылку ""(.*)""")]
        public void GivenЯНажимаюНаСсылку(string p0)
        {
            driver.FindElement(By.XPath("/html/body/main/div/div[1]/div/div/ul[2]/li[2]/a")).Click();
        }
        
        [Given(@"Перехожу на страницу Физ\.Лиц")]
        public void GivenПерехожуНаСтраницуФиз_Лиц()
        {
            driver.FindElement(By.XPath("/html/body/main/div/div[2]/div/h2"));
        }
        
        [When(@"Я нажимаю на ссылку добавление")]
        public void WhenЯНажимаюНаСсылкуДобавление()
        {
            driver.FindElement(By.XPath("/html/body/main/div/div[2]/div/div[1]/div[1]/a/img")).Click();
        }
        
        [Then(@"Вижу страницу создания Физ\.Лица")]
        public void ThenВижуСтраницуСозданияФиз_Лица()
        {
            driver.FindElement(By.XPath("/html/body/main/div/div[2]/h4"));
        }
        
        [Then(@"Я заполняю поля и нажимаю кнопку")]
        public void ThenЯЗаполняюПоляИНажимаюКнопку()
        {
            driver.FindElement(By.Name("Fullname")).SendKeys("Richard Hendricks");
            driver.FindElement(By.Name("Inn")).Clear();
            driver.FindElement(By.Name("Inn")).SendKeys("987365214");
            driver.FindElement(By.Name("DateOfBirth")).SendKeys("01.01.1990");
            driver.FindElement(By.Name("PassportData")).SendKeys("AN7412589");
            driver.FindElement(By.ClassName("selectCountry")).Click();
            driver.FindElement(By.XPath("//*[@id=\"registerInput\"]/option[7]")).Click();
            driver.FindElement(By.Name("Address")).SendKeys("Manasa 23");
            driver.FindElement(By.Name("LegalAddress")).SendKeys("Manasa 23");
            driver.FindElement(By.Name("Position")).SendKeys("worker");
            driver.FindElement(By.ClassName("btn")).Click();
        }
        
        [Then(@"Я вижу страницу со списком Физ\.Лиц")]
        public void ThenЯВижуСтраницуСоСпискомФиз_Лиц()
        {
            driver.FindElement(By.XPath("/html/body/main/div/div[2]/div/h2"));
        }
    }
}
