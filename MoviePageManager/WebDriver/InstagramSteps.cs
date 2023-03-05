using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MoviePageManager.WebDriver
{
	public class InstagramSteps
	{

		private readonly IWebDriver driver;

		public InstagramSteps()
		{
			driver = WebDriverInit.CreateWebDriver();
		}

		public void Login(string username, string password)
		{
			driver.Navigate().GoToUrl("https://www.instagram.com/accounts/login/");
			Thread.Sleep(5000);
			// Enter login credentials
			IWebElement usernameField = driver.FindElement(By.Name("username"));
			IWebElement passwordField = driver.FindElement(By.Name("password"));
			IWebElement loginButton = driver.FindElement(By.CssSelector("button[type='submit']"));
			usernameField.SendKeys(username);
			passwordField.SendKeys(password);
			loginButton.Click();

			Thread.Sleep(5000); // Wait for the page to load

			// Dismiss the "Save Your Login Info?" modal if it appears
			try
			{
				IWebElement notNowButton = driver.FindElement(By.CssSelector("button[type='button'][class='sqdOP yWX7d    y3zKF     ']"));
				notNowButton.Click();
			}
			catch { }
		}
		public void Upload(string movieName, string desc)
		{
			
			// Navigate to the page for uploading a new post
	

			var img = $@"{Environment.CurrentDirectory}\{movieName}";
			// Select the image file to upload
			IWebElement create = driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[1]/div/div/div/div[1]/div[1]/div[1]/div/div/div/div/div[2]/div[7]"));
			create.Click();

			Thread.Sleep(3000);

			IWebElement select = driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[2]/div/div/div[1]/div/div[3]/div/div/div/div/div[2]/div/div/div/div[2]/div[1]/div/div/div[2]/div/button"));
			select.SendKeys(img);

			// Click on the "Share" button
			IWebElement shareButton = driver.FindElement(By.CssSelector("button[type='button'][class='sqdOPyWX7dy3zKF']"));
			shareButton.Click();

			Thread.Sleep(5000);
			driver.Quit();
		}


	}
}
