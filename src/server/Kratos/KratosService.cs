using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System;

namespace KratosDemo.Server.Kratos
{     
    public class KratosService
    {
        private readonly string _kratosUrl;
        private readonly HttpClient _client;

        public KratosService(string kratosUrl)
        {
            _client = new HttpClient();
            _kratosUrl = kratosUrl;
        }

        public async Task<string> GetUserIdByToken(string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_kratosUrl}/sessions/whoami");
            request.Headers.Add("Authorization", token);
            return await SendWhoamiRequestAsync(request);            
        }
        
        public async Task<string> GetUserIdByCookie(string cookieName, string cookieContent)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_kratosUrl}/sessions/whoami");
            request.Headers.Add("Cookie", $"{cookieName}={cookieContent}");
            return await SendWhoamiRequestAsync(request);            
        }

        private async Task<string> SendWhoamiRequestAsync(HttpRequestMessage request)
        {
            var res = await _client.SendAsync(request);
            res.EnsureSuccessStatusCode();

            var json = await res.Content.ReadAsStringAsync();
            var whoami = JsonSerializer.Deserialize<Whoami>(json);    
            if (!whoami.Active)
                throw new InvalidOperationException("Session is not active.");


            return whoami.Identity.Id;
        }
    }
}
