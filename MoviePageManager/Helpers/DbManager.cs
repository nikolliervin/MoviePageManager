using MoviePageManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviePageManager.Helpers
{
	public class DbManager
	{
		private readonly MovieDBContext _context;
		public DbManager(MovieDBContext context)
		{
			_context = context;
		}
		public void addMovie(Movie movie)
		{
			_context.Movies.Add(movie);
			_context.SaveChanges();
		}

		public List<Movie> getMovie()
		{
			return _context.Movies.ToList();
		}

		public List<string> getMovieNames()
		{
			return _context.Movies.Select(m=>m.MovieName).ToList();
		}
	}
}
