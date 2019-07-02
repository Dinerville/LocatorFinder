using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace BrowserInteractions
{
	public class Browser
	{
		private static IWebDriver instance;

		private Browser()
		{

		}

		public static IWebDriver GetInstance()
		{
			return instance ?? (instance = new Browser().InitWebDriver());
		}

		private IWebDriver InitWebDriver()
		{
			Console.WriteLine("Opening Chrome Browser");
			ChromeOptions options = new ChromeOptions();
			options.AddArgument("--incognito");
			options.AddArguments("--window-size=1920,1080");
			return new ChromeDriver(ChromeDriverService.CreateDefaultService(), options, TimeSpan.FromMinutes(2));
		}
	}
}



