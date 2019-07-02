using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BrowserInteractions
{
	public class LocatorParser
	{
		public List<LocatorModel> AllLocators { get; } = new List<LocatorModel>();

		public LocatorParser()
		{
			var pathToFolder = ConfigurationManager.AppSettings["pathToFolder"];
			var paths = Directory.GetFiles(pathToFolder, "*.*", SearchOption.AllDirectories);
			foreach (var path in paths)
			{
				var fileContent = File.ReadAllText(path);
				foreach (var value in Enum.GetValues(typeof(LocatorsTypes)))
				{
					MatchCollection matches;
					if ((LocatorsTypes)value == LocatorsTypes.Actionable)
					{
						 matches = Regex.Matches(fileContent, "ByC\\."+value+ "\\(\"(.*)\"\\).{0,},");
						foreach (Match match in matches)
						{
							AllLocators.Add(new LocatorModel
							{
								Type = LocatorsTypes.XPath,
								Locator = $"//*[@data-actionable='{match.Groups[1].Value.Replace("\\\"", "\"")}']",
								Page = Path.GetFileNameWithoutExtension(path)
							});
						}
						continue;

					}
					matches = Regex.Matches(fileContent, "[B|b]y\\.(?i)" + value.ToString().ToLower() + "(?-i)\\(\"(.+?(?=\"\\)))");
					foreach (Match match in matches)
					{
						AllLocators.Add(new LocatorModel
						{
							Type = (LocatorsTypes)value,
							Locator = match.Groups[1].Value.Replace("\\\"","\""),
							Page = Path.GetFileNameWithoutExtension(path)
						});
					}
				}
				
			}
		}
	}
}
