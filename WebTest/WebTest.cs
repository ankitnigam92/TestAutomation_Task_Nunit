using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace TestAutomation_Task_Nunit.WebTest
{
	public class WebTest
	{
		ChromeDriver driver;
		private WebDriverWait wait;

		string existingUserEmail = "gk123@gk.com";
		string existingUserPassword = "123456";

		[OneTimeSetUp]
		 public void setUp()
		{
			driver = new ChromeDriver();
			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
			driver.Navigate().GoToUrl("http://automationpractice.com/index.php");
		}

		[Test]
		 public void signInTest()
		{
			
			wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("login"))).Click();
			var timestamp = $"{DateTime.Now:yyyyMMdd.HHmm}";
			string email = "gk_" + timestamp + "@gk" + timestamp.Substring(7) + ".com";
			string name = "Firstname";
			string surname = "Lastname";
			driver.FindElement(By.Id("email_create")).SendKeys(email);
			driver.FindElement(By.Id("SubmitCreate")).Click();
			wait.Until(ExpectedConditions.ElementIsVisible(By.Id("id_gender2"))).Click();
			driver.FindElement(By.Id("customer_firstname")).SendKeys(name);
			driver.FindElement(By.Id("customer_lastname")).SendKeys(surname);
			driver.FindElement(By.Id("passwd")).SendKeys("Qwerty");
			SelectElement select = new SelectElement(driver.FindElement(By.Id("days")));
			select.SelectByValue("1");
			select = new SelectElement(driver.FindElement(By.Id("months")));
			select.SelectByValue("1");
			select = new SelectElement(driver.FindElement(By.Id("years")));
			select.SelectByValue("2000");
			driver.FindElement(By.Id("company")).SendKeys("Company");
			driver.FindElement(By.Id("address1")).SendKeys("Qwerty, 123");
			driver.FindElement(By.Id("address2")).SendKeys("zxcvb");
			driver.FindElement(By.Id("city")).SendKeys("Qwerty");
			select = new SelectElement(driver.FindElement(By.Id("id_state")));
			select.SelectByText("Colorado");
			driver.FindElement(By.Id("postcode")).SendKeys("12345");
			driver.FindElement(By.Id("other")).SendKeys("Qwerty");
			driver.FindElement(By.Id("phone")).SendKeys("12345123123");
			driver.FindElement(By.Id("phone_mobile")).SendKeys("12345123123");
			driver.FindElement(By.Id("alias")).SendKeys("gk");
			driver.FindElement(By.Id("submitAccount")).Click();

			IWebElement heading = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("h1")));
			Assert.AreEqual(heading.Text, "MY ACCOUNT");
			Assert.AreEqual(driver.FindElement(By.ClassName("account")).Text, name + " " + surname);
			Assert.True(driver.FindElement(By.ClassName("info-account")).Text.Contains("Welcome to your account."));
			Assert.True(driver.FindElement(By.ClassName("logout")).Displayed);
			Assert.True(driver.Url.Contains("controller=my-account"));
		}

		[Test]
		 public void LogInTest()
		{
			string fullName = "test test";
			wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("login"))).Click();
			driver.FindElement(By.Id("email")).SendKeys(existingUserEmail);
			driver.FindElement(By.Id("passwd")).SendKeys(existingUserPassword);
			driver.FindElement(By.Id("SubmitLogin")).Click();
			IWebElement heading = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("h1")));
			Assert.AreEqual("MY ACCOUNT", heading.Text);
			Assert.AreEqual(fullName, driver.FindElement(By.ClassName("account")).Text);
			Assert.True(driver.FindElement(By.ClassName("info-account")).Text.Contains("Welcome to your account.")); ;
			Assert.True(driver.FindElement(By.ClassName("logout")).Displayed);
			Assert.True(driver.Url.Contains("controller=my-account"));
		}

		[Test]
		public void CheckoutTest()
		{
			wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("login"))).Click();
			driver.FindElement(By.Id("email")).SendKeys(existingUserEmail);
			driver.FindElement(By.Id("passwd")).SendKeys(existingUserPassword);
			driver.FindElement(By.Id("SubmitLogin")).Click();
			wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("Women"))).Click();
			driver.FindElement(By.XPath("//a[@title='Faded Short Sleeve T-shirts']/ancestor::li")).Click();
			driver.FindElement(By.XPath("//a[@title='Faded Short Sleeve T-shirts']/ancestor::li")).Click();
			wait.Until(ExpectedConditions.ElementIsVisible(By.Name("Submit"))).Click();
			wait.Until(ExpectedConditions.ElementIsVisible(
					By.XPath("//*[@id='layer_cart']//a[@class and @title='Proceed to checkout']"))).Click();
			wait.Until(ExpectedConditions.ElementIsVisible(
					By.XPath("//*[contains(@class,'cart_navigation')]/a[@title='Proceed to checkout']"))).Click();
			wait.Until(ExpectedConditions.ElementIsVisible(By.Name("processAddress"))).Click();
			wait.Until(ExpectedConditions.ElementIsVisible(By.Id("uniform-cgv"))).Click();
			driver.FindElement(By.Name("processCarrier")).Click();
			wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("bankwire"))).Click();
			wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='cart_navigation']/button"))).Click();
			IWebElement heading = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("h1")));

			Assert.AreEqual("ORDER CONFIRMATION", heading.Text);

			Assert.True(driver.FindElement(By.XPath("//li[@class='step_done step_done_last four']")).Displayed);
			Assert.True(driver.FindElement(By.XPath("//li[@id='step_end' and @class='step_current last']")).Displayed);
			Assert.True(driver.FindElement(By.XPath("//*[@class='cheque-indent']/strong")).Text
					.Contains("Your order on My Store is complete."));
			Assert.True(driver.Url.Contains("controller=order-confirmation"));
		}

		[TearDown]
		 public void TearDown()
		{
			driver.Close();
			driver.Quit();
		}
	}
}
