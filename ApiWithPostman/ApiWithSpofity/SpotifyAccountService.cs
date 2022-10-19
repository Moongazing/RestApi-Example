using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiWithSpofity
{
  public class SpotifyAccountService : ISpotifyAccountService
  {
    private readonly HttpClient _httpClient;
    public SpotifyAccountService(HttpClient httpClient)
    {
      _httpClient = httpClient;
    }
    public async Task<string> GetToken(string clientId, string clientSecret)
    {
      var request = new HttpRequestMessage(HttpMethod.Post, "token");

      request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
        "Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes("${ clientId }:{ clientScreet}")));


      request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
      {

        {"grant_type","client_credentials" }

      });

      var response = await _httpClient.SendAsync(request);

      response.EnsureSuccessStatusCode();

      var responseStream = await response.Content.ReadAsStreamAsync();

      var authResult = await JsonSerializer.DeserializeAsyc<AuthResult>(responseStream);

      return authResult.access_token;


     
        
    }
  }
}
