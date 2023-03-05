﻿using Models.OpenAI;
using MoviePageManager.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MoviePageManager.Helpers
{
	public class Helpers
	{

		public string firstPrompt()
		{
			return "Suggest me a random movie and its release year in this format Movie: , Year:";
		}
		public string secondPrompt(string movieName, string year)
		{
			return $"Write a thrilling description about {movieName} of year {year} without spolers";
		}

		public string genericPrompt(List<string> movieNames)
		{
			var prompt = "Suggest me a movie in this format Movie: , Year: but not ";

			foreach (var name in movieNames)
			{
				prompt = string.Concat(prompt, ",", name);
			}

			return prompt;
		}

		public string getHashTags(string movieName)
		{
			var prompt = $"Generate 4 popular instagram movie hashtags, and 3 hashtags about the movie {movieName}";

			return prompt;
		}

		public Movie getMovieObj(string response)
		{

			int firstNewLineIndex = response.IndexOf("\n\n");
			if (firstNewLineIndex == -1)
			{
				// fall back to the original code
				int yearStartIndex = response.IndexOf("Year:") + 6;
				string title = response.Substring(7, response.IndexOf(",") - 7).Trim();
				string year = response.Substring(yearStartIndex).Trim();

				return new Movie
				{
					MovieName = title,
					Year = int.Parse(year)
				};
			}
			else
			{
				// use the new code
				var cleanedResp = response.Substring(firstNewLineIndex).Trim('\n');
				int yearStartIndex = cleanedResp.IndexOf("Year:") + 6;
				string title = cleanedResp.Substring(7, cleanedResp.IndexOf(",") - 7).Trim();
				string year = cleanedResp.Substring(yearStartIndex).Trim();

				return new Movie
				{
					MovieName = title,
					Year = int.Parse(year)
				};
			}
		}

		public string getMovieDesc(string responseDesc)
		{	
			if(responseDesc.Contains("\n\n"))
				return responseDesc.Replace("\n\n", string.Empty);
			return responseDesc;
		}

		public string deserializeToString(string response)
		{
			return JsonConvert.DeserializeObject<ResponseBody>(response).Choices[0].Text;
		}


	}

}

