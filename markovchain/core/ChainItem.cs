using System.Collections.Generic;
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

		[JsonProperty("isTail")]
		public bool IsTail { get; }

		public ChainItem(string value, Dictionary<string, int> dependencies)
		{
			Value = value;
			Dependencies = dependencies;
			IsTail = !(dependencies.Count > 0);
		}
	}
}
