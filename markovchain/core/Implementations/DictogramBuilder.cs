using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using core.Interfaces;
using Newtonsoft.Json;

namespace core.Implementations
{
	public class DictogramBuilder : IDictogramBuilder
	{
		private int _windowSize = 1;
		private readonly IEnumerable<string> _sentences;
		private readonly string _input;
		private IEnumerable<ChainItem> _result;

		public DictogramBuilder(string input)
		{
			_input = input;
			_sentences = new TextCutter().Cut(_input);
			_result = new List<ChainItem>();
		}

		public IEnumerable<ChainItem> Build()
		{
			var result = new List<ChainItem>();

			var keys = new HashSet<string>();
			foreach (var sentence in _sentences)
			{
				var words = sentence.Split(' ', '.', ',', '?', '!').Where(w => !string.IsNullOrEmpty(w)).Select(w => w.ToLower()).ToArray();
				foreach (var word in words)
				{
					keys.Add(word);
				}
			}
			using (var progress = new ProgressBar())
			{
				var keysArray = keys.ToArray();
				Parallel.For(0, keysArray.Length, (i) =>
				{
					var key = keysArray[i];
                    try
                    {
                        var nextWords = Regex.Matches(_input, $@"(?<=\b{key}\s)(\w+)");
                        var deps = new Dictionary<string, int>();
                        foreach (Match nextWord in nextWords)
                        {
                            if (!deps.Keys.Contains(nextWord.Value))
                            {
                                deps.Add(nextWord.Value, 1);
                            }
                            else
                            {
                                deps[nextWord.Value]++;
                            }
                        }
                        var newChainItem = new ChainItem(key, deps.OrderByDescending(pair => pair.Value).ToDictionary(kvp => kvp.Key, kvp => kvp.Value));
                        result.Add(newChainItem);
                        
                    }
                    catch (Exception e)
                    {
                    }
                    progress.Report(((double)result.Count / keysArray.Length));
                });
			}
			_result = result;
			return result;
		}

		public async Task SaveJson(string folder)
		{
			var json = JsonConvert.SerializeObject(_result);
			if (!Directory.Exists(folder))
			{
				Directory.CreateDirectory(folder);
			}
			using (FileStream fs = new FileStream($"{folder}\\{Guid.NewGuid().ToString()}.json", FileMode.CreateNew))
			{
				using (var sw = new StreamWriter(fs))
				{
					await sw.WriteLineAsync(json);
				}
			}
		}

		public async Task<IEnumerable<ChainItem>> LoadJson(string folder)
		{
			var result = new List<ChainItem>();
			var files = new DirectoryInfo(folder).GetFiles().Select(fi => fi.FullName);
			foreach (var file in files)
			{
				using (var reader = new StreamReader(file, Encoding.UTF8))
				{
					var json = await reader.ReadToEndAsync();
					var model = JsonConvert.DeserializeObject<IEnumerable<ChainItem>>(json);
					result.AddRange(model);
				}
			}

			return result;
		}
	}
}