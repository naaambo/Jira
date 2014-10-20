using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JIRA
{
	class Program
	{

		static void Main(string[] args)
		{
			RunASync().Wait();
			Console.ReadKey();
		}


		static async Task RunASync()
		{
			using (var client = new HttpClient())
			{

				client.BaseAddress = new Uri("https://icsasoftware.atlassian.net/rest/");
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.Add("Authorization", "Basic " + BasicEncode());

				try
				{
					Console.WriteLine("Enter Your Issue Number e.g 1908");
					var key = Console.ReadLine();

					HttpResponseMessage response = await client.GetAsync("api/2/issue/BPC-" + key);
					var result = response.Content.ReadAsStringAsync().Result;
					Issue issue = JsonConvert.DeserializeObject<Issue>(result);

					Console.WriteLine(response+"\n\n\n\n");
					Console.WriteLine("API-Link:{0}\n\nIssue:{1}\n\nDescription: \n{2}", issue.Self, issue.Key, issue.MyFields.Description);
					response.EnsureSuccessStatusCode();
				}
				catch(HttpRequestException e)
				{
					Console.WriteLine("{0}", e);
				}

			}
		}

		static public string BasicEncode()
		{
			var bytes = Encoding.UTF8.GetBytes("sam.jones@icsasoftware.com:Jira2014");
			var base64 = Convert.ToBase64String(bytes);
			return base64;

		}

	}
}
