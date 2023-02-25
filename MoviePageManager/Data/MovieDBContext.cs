using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviePageManager.Data
{
	public class MovieDBContext : DbContext
	{
		public DbSet<Movie> Movies { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Server=.;Database=UniqueMoviesDB;Trusted_Connection=True;MultipleActiveResultSets=True");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Movie>().HasKey(m => m.Id);
			modelBuilder.Entity<Movie>().Property(m => m.MovieName).IsRequired().HasMaxLength(100);
			modelBuilder.Entity<Movie>().Property(m => m.Year).IsRequired();
		}
	}

}

