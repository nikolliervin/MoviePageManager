using MoviePageManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviePageManager.Helpers
{
	public class MovieCheck
	{	
		private readonly DbManager _dbManager;
		public MovieCheck(DbManager dbManager)
		{
			_dbManager = dbManager;
		}
		public bool existsMovie(string movieName, string year)
		{
			List<Movie> movies = _dbManager.getMovie();

			foreach(var movie in movies)
			{
				if(movie.MovieName == movieName)
				{
					return true;
				}
				
			}

			return false;

		}
	}
}
