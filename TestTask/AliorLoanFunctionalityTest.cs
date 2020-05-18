using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Linq;
using System.Collections.Generic;

namespace TestTask
{
    /// <summary>
    /// This test includes an attempt to fill out a loan application on Alior using the Selenium webdriver
    /// </summary>
    [TestClass]
    public class AliorFunctionalityTests
    {
        

        private IWebDriver driver;  
        private string appURL;
        private bool MaxLengthCheck = false;
        private bool errorBool = false;

        public AliorFunctionalityTests()
        {
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void TheAliorLoanFunctionalityTest()
        {
            

            driver.Navigate().GoToUrl(appURL);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            driver.FindElement(By.Id("firstName")).SendKeys("Maciej");
            driver.FindElement(By.Id("lastName")).SendKeys("Małek");
            System.Threading.Thread.Sleep(500);
            driver.FindElement(By.XPath("//*[@id='app']/div[2]/root/div[4]/div/div[2]/div/a")).Click();
            driver.FindElement(By.Id("emailAddress")).SendKeys("intromalek@gmail.com");
            IWebElement phoneNumberfield = driver.FindElement(By.Id("mobileNumber mobileNumber"));
            phoneNumberfield.SendKeys("000000000");

            if (phoneNumberfield.GetAttribute("value").Length != 9)
            {
                MaxLengthCheck = true;
            }
            if (phoneNumberfield.GetAttribute("value") == "000000000")
            {
                errorBool = true;
            }

            driver.FindElement(By.Id("cashAmount cashAmount")).SendKeys("500");
            driver.FindElement(By.XPath("//*[@id='installmentYears']")).Click();
            System.Threading.Thread.Sleep(500);
            driver.FindElement(By.XPath("/html/body/div[2]/root/div[1]/external/div/modal/div/div/div[2]/external-spinner/div/main/steps/step/spinner-contact-data/div[2]/form/div/ng-transclude/div[6]/div/div[2]/div[1]/div/custom-select/div/span/div/div[2]/div/div[2]")).Click();
            driver.FindElement(By.XPath("//*[@id='installmentMonths']")).Click();
            System.Threading.Thread.Sleep(500);
            driver.FindElement(By.XPath("/html/body/div[2]/root/div[1]/external/div/modal/div/div/div[2]/external-spinner/div/main/steps/step/spinner-contact-data/div[2]/form/div/ng-transclude/div[6]/div/div[2]/div[3]/div/custom-select/div/span/div/div[2]/div/div[12]")).Click();
           
            IWebElement
            informationClausesCheckBox = driver.FindElement(By.XPath("//*[@id='app']/div[2]/root/div[1]/external/div/modal/div/div/div[2]/external-spinner/div/main/steps/step/spinner-contact-data/div[2]/form/div/ng-transclude/div[8]/div/spinner-agreement/fieldset/div[2]/label"));
            informationClausesCheckBox.Click();
            System.Threading.Thread.Sleep(400);
            IWebElement 
            BankCheckBox= driver.FindElement(By.XPath("/html/body/div[2]/root/div[1]/external/div/modal/div/div/div[2]/external-spinner/div/main/steps/step/spinner-contact-data/div[2]/form/div/ng-transclude/div[8]/div/spinner-agreement/fieldset/div[2]/div/div[1]/div[2]"));
            BankCheckBox.Click();
            IWebElement BIKCheckBox= driver.FindElement(By.XPath("/html/body/div[2]/root/div[1]/external/div/modal/div/div/div[2]/external-spinner/div/main/steps/step/spinner-contact-data/div[2]/form/div/ng-transclude/div[8]/div/spinner-agreement/fieldset/div[2]/div/div[2]/div[2]"));
            BIKCheckBox.Click();
            driver.FindElement(By.XPath("//*[@id='app']/div[2]/root/div[1]/external/div/modal/div/div/div[2]/external-spinner/div/main/steps/step/spinner-contact-data/div[2]/form/div/ng-transclude/buttons-control-bottom/div/div/button")).Click();

            /// If at least one of those test passes that means there's and invalid phone number
            Assert.AreNotEqual(true, MaxLengthCheck, "Phone number does not meet the conditions");
            Assert.AreEqual(true, errorBool, "Phone number does not meet the conditions");
            
        }

       
        [TestInitialize()]
        public void SetupTest()
        {
            appURL = " https://wnioski.aliorbank.pl/spinner-process/?partnerId=POR_P_ZERO_S&transactionCode=pozyczki";

            string browser = "Chrome";
            switch (browser)
            {
                case "Chrome":
                    driver = new ChromeDriver(@"C:\Selenium\Chrome");
                    break;
                case "Firefox":
                    driver = new FirefoxDriver();
                    break;
                default:
                    driver = new ChromeDriver();
                    break;
            }

        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            driver.Quit();
        }
    }

}
