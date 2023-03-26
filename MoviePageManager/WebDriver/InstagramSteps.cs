using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using WindowsInput.Native;
using WindowsInput;
using OpenQA.Selenium.Interactions;

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
			var helpers = new Helpers.Helpers();
			var movieCaption = helpers.constructPostText(movieName, desc);
			var act = new Actions(driver);

			var img = $@"{Environment.CurrentDirectory}\{movieName}";
			// Select the image file to upload
			IWebElement create = driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[1]/div/div/div/div[1]/div[1]/div[1]/div/div/div/div/div[2]/div[7]"));
			create.Click();
			
			Thread.Sleep(3000);
			
			IWebElement select = driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[2]/div/div/div[1]/div/div[3]/div/div/div/div/div[2]/div/div/div/div[2]/div[1]/div/div/div[2]/div/button"));
			select.Click();

			WinInputSimulator(img);
			Thread.Sleep(3000);
			// Click on the "Share" button
			
			IWebElement nextBtn = driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[2]/div/div/div[1]/div/div[3]/div/div/div/div/div[2]/div/div/div/div[1]/div/div/div[3]/div/div"));
			nextBtn.Click();
			Thread.Sleep(4000);
			IWebElement nextBtn2 = driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[2]/div/div/div[1]/div/div[3]/div/div/div/div/div[2]/div/div/div/div[1]/div/div/div[3]/div/div"));
			nextBtn2.Click();

			Thread.Sleep(5000);
			IWebElement captionInput = driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[2]/div/div/div[1]/div/div[3]/div/div/div/div/div[2]/div/div/div/div[2]/div[2]/div/div/div/div[2]/div[1]/div[1]"));
			act.MoveToElement(captionInput).Click().Perform();

			act.SendKeys(movieCaption).Perform();
			Thread.Sleep(3000);
			IWebElement postButton = driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[2]/div/div/div[1]/div/div[3]/div/div/div/div/div[2]/div/div/div/div[1]/div/div/div[3]/div/div"));
			postButton.Click();
			Thread.Sleep(3000);
			driver.Quit();
		}

		public void WinInputSimulator(string file)
		{
			InputSimulator inputSimulator = new InputSimulator();

			// Wait for the "Open" dialog box to open
			System.Threading.Thread.Sleep(1000);

			// Click on the "File name" text box
			inputSimulator.Mouse.MoveMouseTo(100, 100); // Change coordinates to match your system
			inputSimulator.Mouse.LeftButtonClick();

			// Simulate typing the file path and pressing Enter
			inputSimulator.Keyboard.TextEntry(file);
			inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
		}



	}
}
