using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace MoviePageManager.MovieDB
{
	
	public class MovieDBService
	{
		private readonly string _apiKey;
		private readonly HttpClient _client;
		public MovieDBService(string apiKey)
		{
			HttpClientHandler clientHandler = new HttpClientHandler();
			clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };


			_apiKey = apiKey;
			_client = new HttpClient
			{
				BaseAddress = new Uri("https://api.themoviedb.org/3/"),
				
			};
			_client.DefaultRequestHeaders.Accept.Clear();
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
		}

		public async Task getMovieImg(string movie, string year)
		{
			string url = $"search/movie?api_key={_apiKey}&query={movie.Replace(" ", "%")}&year={year}";


			HttpResponseMessage response = await _client.GetAsync(url);
			string json = await response.Content.ReadAsStringAsync();

			string imagePath = null;
			dynamic data = JsonSerializer.Deserialize<dynamic>(json);

			JObject dataLink = JObject.Parse(json);
			JToken backdropPathToken = dataLink["results"]?.FirstOrDefault()?["backdrop_path"];
			imagePath = backdropPathToken?.ToString();


			if (!string.IsNullOrEmpty(imagePath))
			{
				string imageUrl = $"https://image.tmdb.org/t/p/original{imagePath}";
				response = await _client.GetAsync(imageUrl);
				Thread.Sleep(1000);
				using (Stream stream = await response.Content.ReadAsStreamAsync())
				using (FileStream fileStream = new FileStream($"{movie}.jpg", FileMode.Create, FileAccess.Write))
				{
					await stream.CopyToAsync(fileStream);
				}
				
			}
			else
			{
				throw new Exception("Image not found");
			}
		}

	}
}
