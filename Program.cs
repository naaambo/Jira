using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace JIRA
{
	class Program
	{
		public class Issue
		{
			public string Expand { get; set; }
			public double ID { get; set; }
			public string Self { get; set; }
			public Fields MyFields { get; set; }

			public Issue()
			{
				MyFields = new Fields();
			}
			//TODO: Get access to Fields:Description
			public class Fields
			{
				public string Description { get; set; }
			}
		}

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
					HttpResponseMessage response = await client.GetAsync("api/2/issue/BPC-1908");
					Issue issue = await response.Content.ReadAsAsync<Issue>();
					Console.WriteLine(response+"\n\n\n\n");
					Console.WriteLine("{0}\n{1}\n{2}", issue.ID, issue.Self, issue.MyFields.Description);
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
