using System.Text.Json.Serialization;

namespace KratosDemo.Server.Kratos
{
    public class Whoami
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("identity")]
        public Identity Identity { get; set; }
    }

    public class Identity
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}
