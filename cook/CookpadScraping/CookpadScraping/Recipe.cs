using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;
using Dapper;

namespace CookpadScraping
{
	public class Recipe
	{
		public Recipe()
		{
			Ingredients = new List<Ingredient>();
			Steps = new List<string>();
		}

		public string Url { get; set; }

		public string Title { get; set; }
		public List<Ingredient> Ingredients { get; set; }
		public List<string> Steps { get; set; }
		public string Point { get; set; }

		public string IX { get; set; }	// DBからXMLを読み込む時に使用
		public string SX { get; set; }  // DBからXMLを読み込む時に使用

		public string IngredientsToXml()
		{
			using (var write = new StringWriter())
			{
				var serializer = new XmlSerializer(typeof(List<Ingredient>));
				serializer.Serialize(write, this.Ingredients);
				return write.ToString();
			}
		}
		public static List<Ingredient> XmlToIngredients(string xml)
		{
			using (var reader = new StringReader(xml))
			{
				var serializer = new XmlSerializer(typeof(List<Ingredient>));
				return (List<Ingredient>)serializer.Deserialize(reader);
			}
		}

		public string StepsToXml()
		{
			using (var write = new StringWriter())
			{
				var serializer = new XmlSerializer(typeof(List<string>));
				serializer.Serialize(write, this.Steps);
				return write.ToString();
			}
		}
		public static List<string> XmlToSteps(string xml)
		{
			using (var reader = new StringReader(xml))
			{
				var serializer = new XmlSerializer(typeof(List<string>));
				return (List<string>)serializer.Deserialize(reader);
			}
		}

		public static bool Any(string url)
		{
			try
			{
				using (var cn = DbConnectionFactory.Create("DefaultConnection"))
				{
					cn.Open();
					var count = cn.ExecuteScalar<int>("select count(*) from Recipe where Url=@Url", new { Url = url });
					return count > 0;
				}
			}
			catch
			{
			}
			return false;
		}

		public void Save()
		{
			try
			{
				using (var cn = DbConnectionFactory.Create("DefaultConnection"))
				{
					cn.Open();
					cn.Execute("delete Recipe where Url=@Url", new { Url = this.Url });
					cn.Execute("insert into Recipe values (@Url,@Title,@Ingredients,@Steps,@Point)",
						new { this.Url, this.Title, Ingredients = this.IngredientsToXml(), Steps = this.StepsToXml(), this.Point });
				}
			}
			catch
			{

			}
		}

		public static void LoadToFile(string path)
		{
			try
			{
				using (var cn = DbConnectionFactory.Create("DefaultConnection"))
				{
					cn.Open();

					var query = "select Url,Title,Ingredients as IX,Steps as SX,Point from Recipe";
					var datas = cn.Query<Recipe>(query);

					using (var titleStream = new StreamWriter(Path.Combine(path, @"title.txt"), false, Encoding.UTF8))
					using (var Ingredients1Stream = new StreamWriter(Path.Combine(path, @"ingredients1.txt"), false, Encoding.UTF8))
					using (var Ingredients2Stream = new StreamWriter(Path.Combine(path, @"ingredients2.txt"), false, Encoding.UTF8))
					using (var Ingredients3Stream = new StreamWriter(Path.Combine(path, @"ingredients3.txt"), false, Encoding.UTF8))
					using (var Ingredients4Stream = new StreamWriter(Path.Combine(path, @"ingredients4.txt"), false, Encoding.UTF8))
					using (var Ingredients5Stream = new StreamWriter(Path.Combine(path, @"ingredients5.txt"), false, Encoding.UTF8))
					using (var Ingredients6Stream = new StreamWriter(Path.Combine(path, @"ingredients6.txt"), false, Encoding.UTF8))
					using (var Ingredients7Stream = new StreamWriter(Path.Combine(path, @"ingredients7.txt"), false, Encoding.UTF8))
					using (var Ingredients8Stream = new StreamWriter(Path.Combine(path, @"ingredients8.txt"), false, Encoding.UTF8))
					using (var Ingredients9Stream = new StreamWriter(Path.Combine(path, @"ingredients9.txt"), false, Encoding.UTF8))
					using (var Ingredients0Stream = new StreamWriter(Path.Combine(path, @"ingredients0.txt"), false, Encoding.UTF8))
					using (var Step0Stream = new StreamWriter(Path.Combine(path, @"step0.txt"), false, Encoding.UTF8))
					using (var Step1Stream = new StreamWriter(Path.Combine(path, @"step1.txt"), false, Encoding.UTF8))
					using (var Step2Stream = new StreamWriter(Path.Combine(path, @"step2.txt"), false, Encoding.UTF8))
					using (var Step3Stream = new StreamWriter(Path.Combine(path, @"step3.txt"), false, Encoding.UTF8))
					using (var Step4Stream = new StreamWriter(Path.Combine(path, @"step4.txt"), false, Encoding.UTF8))
					using (var Step5Stream = new StreamWriter(Path.Combine(path, @"step5.txt"), false, Encoding.UTF8))
					using (var PointStream = new StreamWriter(Path.Combine(path, @"point.txt"), false, Encoding.UTF8))
						foreach (var d in datas)
						{
							titleStream.WriteLine(HttpUtility.HtmlDecode(d.Title));
							var ingredients = XmlToIngredients(d.IX);
							if (ingredients.Count > 0) Ingredients0Stream.WriteLine(HttpUtility.HtmlDecode(ingredients[0].Name) + " " + HttpUtility.HtmlDecode(ingredients[0].Weight));
							if (ingredients.Count > 1) Ingredients1Stream.WriteLine(HttpUtility.HtmlDecode(ingredients[1].Name) + " " + HttpUtility.HtmlDecode(ingredients[1].Weight));
							if (ingredients.Count > 2) Ingredients2Stream.WriteLine(HttpUtility.HtmlDecode(ingredients[2].Name) + " " + HttpUtility.HtmlDecode(ingredients[2].Weight));
							if (ingredients.Count > 3) Ingredients3Stream.WriteLine(HttpUtility.HtmlDecode(ingredients[3].Name) + " " + HttpUtility.HtmlDecode(ingredients[3].Weight));
							if (ingredients.Count > 4) Ingredients4Stream.WriteLine(HttpUtility.HtmlDecode(ingredients[4].Name) + " " + HttpUtility.HtmlDecode(ingredients[4].Weight));
							if (ingredients.Count > 5) Ingredients5Stream.WriteLine(HttpUtility.HtmlDecode(ingredients[5].Name) + " " + HttpUtility.HtmlDecode(ingredients[5].Weight));
							if (ingredients.Count > 6) Ingredients6Stream.WriteLine(HttpUtility.HtmlDecode(ingredients[6].Name) + " " + HttpUtility.HtmlDecode(ingredients[6].Weight));
							if (ingredients.Count > 7) Ingredients7Stream.WriteLine(HttpUtility.HtmlDecode(ingredients[7].Name) + " " + HttpUtility.HtmlDecode(ingredients[7].Weight));
							if (ingredients.Count > 8) Ingredients8Stream.WriteLine(HttpUtility.HtmlDecode(ingredients[8].Name) + " " + HttpUtility.HtmlDecode(ingredients[8].Weight));
							if (ingredients.Count > 9) Ingredients9Stream.WriteLine(HttpUtility.HtmlDecode(ingredients[9].Name) + " " + HttpUtility.HtmlDecode(ingredients[9].Weight));
							var steps = XmlToSteps(HttpUtility.HtmlDecode(d.SX));
							if (steps.Count > 0) Step0Stream.WriteLine(HttpUtility.HtmlDecode(steps[0]));
							if (steps.Count > 1) Step1Stream.WriteLine(HttpUtility.HtmlDecode(steps[1]));
							if (steps.Count > 2) Step2Stream.WriteLine(HttpUtility.HtmlDecode(steps[2]));
							if (steps.Count > 3) Step3Stream.WriteLine(HttpUtility.HtmlDecode(steps[3]));
							if (steps.Count > 4) Step4Stream.WriteLine(HttpUtility.HtmlDecode(steps[4]));
							if (steps.Count > 5) Step5Stream.WriteLine(HttpUtility.HtmlDecode(steps[5]));
							if (!string.IsNullOrEmpty(d.Point)) PointStream.WriteLine(HttpUtility.HtmlDecode(d.Point));
						}
				}
			}
			catch
			{

			}
		}
	}

	public class Ingredient
	{
		public string Name { get; set; }
		public string Weight { get; set; }
	}
}

