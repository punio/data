using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CookpadScraping
{
	class Program
	{
		static void Main(string[] args)
		{
			// Web -> DB
			GetAsync().Wait();

			// DB -> Text
			//Export();
		}

		#region Get
		static Task GetAsync() => Task.Run(async () =>
		{
			const string baseUrl = "http://cookpad.com/recipe/";
			const int indexBase = 1100590;
			const int scrapingCount = 10000;
			try
			{
				for (var i = 0; i < scrapingCount; i++)
				{
					if ((i % 100) == 0) Console.WriteLine($"Step {i}");
					var doc = new HtmlAgilityPack.HtmlDocument();

					var recipe = new Recipe();
					recipe.Url = $"{baseUrl}{indexBase + i}";

					if (Recipe.Any(recipe.Url)) continue;


					try
					{
						using (var client = new HttpClient())
						using (var stream = await client.GetStreamAsync(new Uri(recipe.Url)))
						{
							doc.Load(stream, Encoding.UTF8);
						}
					}
					catch (Exception exp)
					{
						Console.WriteLine(exp.Message);
						continue;
					}

					var title = doc.DocumentNode.SelectNodes("//*[contains(@class,'recipe-title')]")?.ElementAt(0)?.InnerText;
					if (string.IsNullOrEmpty(title)) continue;
					recipe.Title = title.Trim('\n', '\t', '\r');

					var ingredients = doc.GetElementbyId("ingredients_list")?.ChildNodes?.Where(n => n.Name == "div");
					if (ingredients == null) continue;
					foreach (var ingredient in ingredients)
					{
						var children = ingredient.ChildNodes.Where(n => n.Name == "div");
						if (children.Count() < 2) continue;
						var d = new Ingredient();
						d.Name = children.ElementAt(0).InnerText.Trim('\n', '\t', '\r');
						d.Weight = children.ElementAt(1).InnerText.Trim('\n', '\t', '\r');
						recipe.Ingredients.Add(d);
					}
					var steps = doc.DocumentNode.SelectNodes("//*[contains(@class,'step_text')]")?.Select(n => n.InnerText);
					if (steps == null) continue;
					foreach (var step in steps)
					{
						recipe.Steps.Add(step.Trim('\n', '\t', '\r'));
					}
					var advice = doc.GetElementbyId("advice");
					recipe.Point = advice?.InnerText?.Trim('\n', '\t', '\r');

					recipe.Save();

					await Task.Delay(1000);
				}

				Console.WriteLine("Complete");
				Console.ReadLine();
			}
			catch (Exception exp)
			{
				Console.WriteLine(exp.Message);
			}
		});
		#endregion

		#region Export to file
		private static void Export() => Recipe.LoadToFile(@"F:\My\Desktop\chainer-char-rnn-master\data");
		#endregion
	}
}
