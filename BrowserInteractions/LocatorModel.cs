using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace BrowserInteractions
{
	public class LocatorModel
	{
		public LocatorsTypes Type { get; set; }
		public string Locator { get; set; }
		public string Page { get; set; }
	}
}
