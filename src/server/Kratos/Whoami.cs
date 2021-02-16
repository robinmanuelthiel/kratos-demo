using System;
using System.Text.Json.Serialization;

namespace KratosDemo.Server.Kratos
{
    public class Whoami
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } 

        [JsonPropertyName("active")]
        public bool Active { get; set; } 

        [JsonPropertyName("expires_at")]
        public DateTime ExpiresAt { get; set; } 

        [JsonPropertyName("authenticated_at")]
        public DateTime AuthenticatedAt { get; set; } 

        [JsonPropertyName("issued_at")]
        public DateTime IssuedAt { get; set; } 

        [JsonPropertyName("identity")]
        public Identity Identity { get; set; } 
    }

    public class Identity
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}
