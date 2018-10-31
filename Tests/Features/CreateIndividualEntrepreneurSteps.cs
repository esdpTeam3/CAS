using System;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Tests.Features;

namespace Tests
{
    [Binding]
    public class CreateIndividualEntrepreneurSteps
    {
        private IWebDriver driver;

        public CreateIndividualEntrepreneurSteps()
        {
            driver = Driver.GetDriver;
        }

        [Given(@"Я нажимаю на ссылку ИП")]
        public void GivenЯНажимаюНаСсылкуИП()
        {
            driver.FindElement(By.XPath("/html/body/main/div/div[1]/div/div/ul[2]/li[3]/a")).Click();
        }
        
        [Given(@"Перехожу на страницу ИП")]
        public void GivenПерехожуНаСтраницуИП()
        {
            driver.FindElement(By.XPath("/html/body/main/div/div[2]/h2"));
        }
        
        [When(@"Я нажимаю на ссылку создания")]
        public void WhenЯНажимаюНаСсылкуСоздания()
        {
            driver.FindElement(By.XPath("/html/body/main/div/div[2]/div[1]/div[1]/a/img")).Click();
        }
        
        [Then(@"Вижу страницу создания ИП")]
        public void ThenВижуСтраницуСозданияИП()
        {
            driver.FindElement(By.XPath("/html/body/main/div/div[2]/h4"));
        }
        
        [Then(@"Я заполняю все поля и нажимаю кнопку")]
        public void ThenЯЗаполняюВсеПоляИНажимаюКнопку()
        {
            driver.FindElement(By.Name("Fullname")).SendKeys("Gavin Bellson");
            driver.FindElement(By.Name("Inn")).Clear();
            driver.FindElement(By.Name("Inn")).SendKeys("698453217");
            driver.FindElement(By.Name("DateOfBirth")).SendKeys("13.03.1973");
            driver.FindElement(By.Name("PassportData")).SendKeys("ID2589631");
            driver.FindElement(By.ClassName("selectCountries")).Click();
            driver.FindElement(By.XPath("//*[@id=\"registerInput\"]/option[8]")).Click();
            driver.FindElement(By.Name("Address")).SendKeys("Jibek Jolu 7");
            driver.FindElement(By.Name("LegalAddress")).SendKeys("Jibek Jolu 7");
            driver.FindElement(By.ClassName("btn")).Click();
        }
        
        [Then(@"Я вижу страницу со списком ИП")]
        public void ThenЯВижуСтраницуСоСпискомИП()
        {
            driver.FindElement(By.XPath("/html/body/main/div/div[2]/h2"));
        }
    }
}
