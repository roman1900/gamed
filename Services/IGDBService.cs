using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace gamed
{
public class IGDBService : IIGDBService
{
    private readonly HttpClient _httpClient;
    private readonly string _remoteServiceBaseUrl;

    private twitchToken _twitchToken;
    struct twitchToken {
        string access_token;
        int expires_in;
        string token_type; 
    }
    public IGDBService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        var t = Task.Run(() => _httpClient.PostAsync("https://id.twitch.tv/oauth2/token?client_id=nelijtsar5hu0ojf3zwlgjijtxvoe0&client_secret=***REMOVED***&grant_type=client_credentials",null));
        t.Wait();
        _twitchToken = JsonConvert.DeserializeObject<twitchToken>(t.Result.Content.ToString());
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