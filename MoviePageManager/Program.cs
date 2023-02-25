using MoviePageManager.OpenAI;
using System;
using Models.OpenAI;
using System.Configuration;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MoviePageManager
{
	public class Program
	{
	
		static async Task Main(string[] args)
		{
			var openAIKey = Environment.GetEnvironmentVariable("openAIKey");

			var _openAIService = new OpenAIService(openAIKey);

			var prompt = "Give me a movie name with a ration of over 8.5";


			var response = await _openAIService.SendRequestAsync(prompt);

			var choiceObj = JsonConvert.DeserializeObject<ResponseBody>(response).Choices[0];

			var nextPromp = "Write a summary about the movie:" + choiceObj.Text + " without spoliers";

			var secondResp = await _openAIService.SendRequestAsync(nextPromp);

		}
	}
}
