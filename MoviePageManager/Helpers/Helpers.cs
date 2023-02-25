using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviePageManager.Helpers
{
	public class PromptGenerator
	{	

		public string firstPrompt()
		{
			return "Suggest me a random movie and its release year in this format Movie: , Year:";
		}
		public string secondPrompt(string movieName,string year)
		{
			return $"Write a thrilling description about {movieName} of year {year} without spolers";
		}

		public string uniqueMovieNamePrompt()
		{
			return "";
		}
	}
}
