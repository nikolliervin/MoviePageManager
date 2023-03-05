using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

public class WebDriverInit
{
	public static IWebDriver CreateWebDriver()
	{
		ChromeOptions options = new ChromeOptions();
		

		return new ChromeDriver(options);
	}
}
