using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrowserInteractions;

namespace FindLocators
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			var url = ConfigurationManager.AppSettings["startUrl"];
			var driver = Browser.GetInstance();
			driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 0, 0);
			driver.Navigate().GoToUrl(url);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
			
		}
	}
}
