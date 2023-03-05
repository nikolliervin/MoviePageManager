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
using MoviePageManager.MovieDB;
using MoviePageManager.WebDriver;

namespace MoviePageManager
{
	public class Program
	{
		

		static async Task Main(string[] args)
		{
			var openAIKey = Environment.GetEnvironmentVariable("openAIKey");
			var theMovieDbKey = Environment.GetEnvironmentVariable("tmdbKey");
			var user = Environment.GetEnvironmentVariable("userIg");
			var pass = Environment.GetEnvironmentVariable("passIg");

			var tmdbService = new MovieDBService(theMovieDbKey);
			var _openAIService = new OpenAIService(openAIKey);
			var helpers = new Helpers.Helpers();
			using var dbContext = new MovieDBContext();
			var dbManager = new DbManager(dbContext);
			var existsMovie = new MovieCheck(dbManager);
			var steps = new InstagramSteps();

			var prompt = helpers.firstPrompt();

			var firstResponse = await _openAIService.SendRequestAsync(prompt);
			var choiceObj = helpers.deserializeToString(firstResponse);
			var movie = helpers.getMovieObj(choiceObj);

			if (dbManager.getMovie().Count != 0)
				while (existsMovie.existsMovie(movie.MovieName, movie.Year.ToString()))
				{
					var newPrompt = helpers.genericPrompt(dbManager.getMovieNames());
					var altResp = await _openAIService.SendRequestAsync(newPrompt);
					var newChoiceObj = helpers.deserializeToString(altResp);
					movie = helpers.getMovieObj(newChoiceObj);
				}
					
			dbManager.addMovie(movie);

			var nextPrompt = helpers.secondPrompt(movie.MovieName, movie.Year.ToString());
			var secondResp = await _openAIService.SendRequestAsync(nextPrompt);
			var descObj = helpers.deserializeToString(secondResp);
			var desc = helpers.getMovieDesc(descObj);

			await tmdbService.getMovieImg(movie.MovieName,movie.Year.ToString());

			steps.Login(user, pass);
			steps.Upload(movie.MovieName, desc);
		}

	
	}
}
