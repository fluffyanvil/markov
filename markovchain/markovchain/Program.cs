using System;
using core.Helpers;
using core.Implementations;
using core.Interfaces;

namespace markovchain
{
	class Program
	{
		private const string Root = "..\\..\\..\\";
		private static string _samplesFolder = $"{Root}samples";
		private static readonly string _models = $"{Root}models";

		static void Main(string[] args)
		{
			ITextLoader tl = new TextLoader();
			var s = tl.Load(_samplesFolder);
			var rs = RegexHelper.Clean(s);
			var db = new DictogramBuilder(rs);

			//db.Build();
			var l = db.LoadJson(_models).Result;
			//db.SaveJson(_models).Wait();

			Console.ReadLine();
		}
	}
}