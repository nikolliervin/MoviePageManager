using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Models.OpenAI;
using Newtonsoft.Json;

namespace MoviePageManager.OpenAI
{
	public class OpenAIService
	{
		private readonly HttpClient _client;
		private readonly string _apiKey;

		public OpenAIService(string apiKey)
		{
			_apiKey = apiKey;
			_client = new HttpClient
			{
				BaseAddress = new Uri("https://api.openai.com/v1/completions")
			};
			_client.DefaultRequestHeaders.Accept.Clear();
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
		}

		public async Task<string> SendRequestAsync(string prompt)
		{
			var request = new RequestBody()
			{
				model = ConfigurationManager.AppSettings["model"],
				prompt = prompt,
				max_tokens = int.Parse(ConfigurationManager.AppSettings["maxTokens"]),
				temperature = int.Parse(ConfigurationManager.AppSettings["temperature"]),
				top_p = int.Parse(ConfigurationManager.AppSettings["top_p"]),
				n = int.Parse(ConfigurationManager.AppSettings["n"]),
				stream = bool.Parse(ConfigurationManager.AppSettings["stream"]),
				stop = ConfigurationManager.AppSettings["stop"]
			};

			var requestContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

			var response = await _client.PostAsync("", requestContent);
			if (response.IsSuccessStatusCode)
			{
				var responseContent = await response.Content.ReadAsStringAsync();
				return responseContent;
			}
			else
			{
			
					throw new HttpRequestException($"Request failed with status code {response.StatusCode}: {await response.Content.ReadAsStringAsync()}");
			}
		}



	}
}
