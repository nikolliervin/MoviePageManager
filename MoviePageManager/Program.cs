using MoviePageManager.OpenAI;
using System;
using Models.OpenAI;
using System.Configuration;
using System.Threading.Tasks;

namespace MoviePageManager
{
	public class Program
	{
	
		static async Task Main(string[] args)
		{
			var openAIKey = Environment.GetEnvironmentVariable("openAIKey");

			var _openAIService = new OpenAIService(openAIKey);

			var prompt = "Give me a movie name with a ration of over 8.5";





			await _openAIService.SendRequestAsync(prompt);


		}
	}
}
