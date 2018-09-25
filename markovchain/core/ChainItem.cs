using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace core
{
	[JsonObject]
	public class ChainItem
	{
		[JsonProperty("value")]
		public string Value { get; }

		[JsonProperty("dependencies")]
		public Dictionary<string, int> Dependencies { get; set; }

		public ChainItem(string value, Dictionary<string, int> dependencies)
		{
			Value = value;
			Dependencies = dependencies;
		}
	}
}
