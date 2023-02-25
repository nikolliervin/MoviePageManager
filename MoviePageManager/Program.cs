using MoviePageManager.OpenAI;
using System;
using Models.OpenAI;
using System.Configuration;

namespace MoviePageManager
{
	public class Program
	{
	
		static void Main(string[] args)
		{
			var openAIKey = Environment.GetEnvironmentVariable("openAIKey");

			var _openAIService = new OpenAIService(openAIKey);



			var request = new RequestBody()
			{
				model = ConfigurationManager.AppSettings[openAIKey]
			};


		}
	}
}
