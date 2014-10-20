using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JIRA
{
	public class Issue
	{
		[JsonProperty("expand")]
		public string Expand { get; set; }
		[JsonProperty("iD")]
		public double ID { get; set; }
		[JsonProperty("key")]
		public string Key { get; set; }
		[JsonProperty("self")]
		public string Self { get; set; }
		[JsonProperty("fields")]
		public Fields MyFields { get; set; }

		//TODO: Get access to Fields:Description
		public class Fields
		{
			[JsonProperty("summary")]
			public string Summary { get; set; }
			[JsonProperty("description")]
			public string Description { get; set; }
		}
	}
}
