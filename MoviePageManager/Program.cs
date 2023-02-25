using MoviePageManager.OpenAI;
using System;
using Models.OpenAI;
using System.Configuration;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MoviePageManager.Helpers;
using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;
using MoviePageManager.Data;

namespace MoviePageManager
{
	public class Program
	{
		

		static async Task Main(string[] args)
		{
			var openAIKey = Environment.GetEnvironmentVariable("openAIKey");

			var _openAIService = new OpenAIService(openAIKey);
			var helpers = new Helpers.Helpers();
			using var dbContext = new MovieDBContext();
			var dbManager = new DbManager(dbContext);


			var prompt = helpers.firstPrompt();


			var firstResponse = await _openAIService.SendRequestAsync(prompt);

			var choiceObj = JsonConvert.DeserializeObject<ResponseBody>(firstResponse).Choices[0].Text;

			var movie = helpers.getMovieObj(choiceObj);

			
			dbManager.addMovie(movie);

			var nextPrompt = helpers.secondPrompt(movie.MovieName, movie.Year.ToString());

			var secondResp = await _openAIService.SendRequestAsync(nextPrompt);

		}

	
	}
}
