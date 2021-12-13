using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System;
using gamed.Models;
namespace gamed
{
	public class IGDBService : IIGDBService
	{
		private readonly HttpClient _httpClient;
		private readonly string _baseURL = "https://api.igdb.com/v4";
		private twitchToken _twitchToken;
		private twitchCredentials _twitchCredentials;
		public struct twitchToken
		{
			public string accessToken;
			public int expiresIn;
			public string tokenType;
			private DateTime _dateExpires;
			[JsonConstructor]
			public twitchToken(string access_token, int expires_in, string token_type)
			{
				accessToken = access_token;
				expiresIn = expires_in;
				tokenType = token_type;
				_dateExpires = DateTime.Now.AddSeconds(expiresIn);
			}
			public bool expired()
			{
				if (_dateExpires <= DateTime.MinValue || _dateExpires < DateTime.Now)
					return true;
				return false;
			}
		}
		public struct twitchCredentials
		{
			public string clientID;
			public string clientSecret;
		}
		public IGDBService(HttpClient httpClient)
		{
			_httpClient = httpClient;

			if (_twitchToken.expired())
			{
				_twitchCredentials = JsonConvert.DeserializeObject<twitchCredentials>(File.ReadAllText("twitch.json"));
				var t = Task.Run(() => _httpClient.PostAsync("https://id.twitch.tv/oauth2/token?client_id=" + _twitchCredentials.clientID + "&client_secret=" + _twitchCredentials.clientSecret + "&grant_type=client_credentials", null));
				t.Wait();
				var c = Task.Run(() => t.Result.Content.ReadAsStringAsync());
				c.Wait();
				_twitchToken = JsonConvert.DeserializeObject<twitchToken>(c.Result);
				_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _twitchToken.accessToken);
			}
		}
		public async Task<List<PlatformModel>> GetPlatforms()
		{
			var uri = _baseURL + "/platforms";
			HttpContent content = new StringContent("fields *;\nlimit 500;");


			content.Headers.Add("Client-ID", _twitchCredentials.clientID);

			var response = await _httpClient.PostAsync(uri, content);
			var rr = await response.Content.ReadAsStringAsync();
			var platform = JsonConvert.DeserializeObject<List<PlatformModel>>(rr);
			return platform;
		}
		// public async Task<Catalog> GetCatalogItems(int page, int take,
		//                                            int? brand, int? type)
		// {
		//     var uri = API.Catalog.GetAllCatalogItems(_remoteServiceBaseUrl,
		//                                              page, take, brand, type);

		//     var responseString = await _httpClient.GetStringAsync(uri);

		//     var catalog = JsonConvert.DeserializeObject<Catalog>(responseString);
		//     return catalog;
		// }
	}
    
}