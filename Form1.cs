using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrowserInteractions;
using OpenQA.Selenium;

namespace FindLocators
{
	public partial class Form1 : Form
	{
		private int Count { get; set; } = 0;

		public Form1()
		{
			InitializeComponent();
		}

		private void AddItem(IReadOnlyCollection<IWebElement> elem ,LocatorModel item)
		{
			if (elem.Count > 0 )
			{
				var isDisplayed = elem.ToList()[0].Displayed;
				if (isDisplayed)
				{
					Count++;
					listBox1.Items.Add($"{item.Locator} || {item.Type} || {item.Page}");

				}
			}
			
		}

		private void button1_Click(object sender, EventArgs e)
		{
			listBox1.Items.Clear();

			var collection = new LocatorParser().AllLocators;

			Count = 0;
			foreach (var item in collection)
			{
				IReadOnlyCollection<IWebElement> elem;
				
				try
				{
					switch (item.Type)
					{
						case LocatorsTypes.ClassName:
							elem = Browser.GetInstance().FindElements(By.ClassName(item.Locator));
							AddItem(elem,item);
							break;
						case LocatorsTypes.CssSelector:
							elem = Browser.GetInstance().FindElements(By.CssSelector(item.Locator));
							AddItem(elem, item);
							break;
						case LocatorsTypes.Id:
							elem = Browser.GetInstance().FindElements(By.Id(item.Locator));
							AddItem(elem, item);
							break;
						case LocatorsTypes.LinkText:
							elem = Browser.GetInstance().FindElements(By.LinkText(item.Locator));
							AddItem(elem, item);
							break;
						case LocatorsTypes.Name:
							elem = Browser.GetInstance().FindElements(By.Name(item.Locator));
							AddItem(elem, item);
							break;
						case LocatorsTypes.PartialLinkText:
							elem = Browser.GetInstance().FindElements(By.PartialLinkText(item.Locator));
							AddItem(elem, item);
							break;
						case LocatorsTypes.TagName:
							elem = Browser.GetInstance().FindElements(By.TagName(item.Locator));
							AddItem(elem, item);
							break;
						case LocatorsTypes.XPath:
							elem = Browser.GetInstance().FindElements(By.XPath(item.Locator));
							AddItem(elem, item);
							break;
					}
					
				}
				catch (Exception exception)
				{
					listBox1.Items.Add($"{item.Locator} || {item.Type} || {item.Page} || Invalid Item");
				}

				
				
			}
			listBox1.Items.Add($"found {Count} locators");
			if (Count == 0)
			{
				listBox1.Items.Add("No locators found");
			}
		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{

			
			var send = (ListBox) sender;
			var dataString = send.SelectedItem.ToString();
			var data = dataString.Split(new string[] {" || "}, StringSplitOptions.None);
			IJavaScriptExecutor executor = (IJavaScriptExecutor)Browser.GetInstance();
			var enumItem = Enum.Parse(typeof(LocatorsTypes), data[1]);
			IWebElement element;
			switch (enumItem)
			{
				case LocatorsTypes.ClassName:
					element = Browser.GetInstance().FindElement(By.ClassName(data[0]));
					break;
				case LocatorsTypes.CssSelector:
					element = Browser.GetInstance().FindElement(By.CssSelector(data[0]));
					break;
				case LocatorsTypes.Id:
					element = Browser.GetInstance().FindElement(By.Id(data[0]));
					break;
				case LocatorsTypes.LinkText:
					element = Browser.GetInstance().FindElement(By.LinkText(data[0]));
					break;
				case LocatorsTypes.Name:
					element = Browser.GetInstance().FindElement(By.Name(data[0]));
					break;
				case LocatorsTypes.PartialLinkText:
					element = Browser.GetInstance().FindElement(By.PartialLinkText(data[0]));
					break;
				case LocatorsTypes.TagName:
					element = Browser.GetInstance().FindElement(By.TagName(data[0]));
					break;
				case LocatorsTypes.XPath:
					element = Browser.GetInstance().FindElement(By.XPath(data[0]));
					break;
				default:
					throw new Exception("no such type");
			}

			executor.ExecuteScript("arguments[0].setAttribute('style', 'background: blue; border: 2px solid red;');", element);
			executor.ExecuteScript("arguments[0].scrollIntoView(true);", element);
			Clipboard.SetText(data[0]);
			}
			catch (Exception exception)
			{
				listBox2.Items.Add(exception.ToString());
			}
			
		}

		private void label1_Click(object sender, EventArgs e)
		{
			Browser.GetInstance().Navigate().GoToUrl("https://dnaumov.com");
		}

		private void Form1_Closing(object sender, CancelEventArgs e)
		{
			Browser.GetInstance().Quit();
		}
	}
}
