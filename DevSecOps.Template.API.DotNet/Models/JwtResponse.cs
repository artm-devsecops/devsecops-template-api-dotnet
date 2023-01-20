using System.Text.Json.Serialization;

namespace DevSecOps.Template.API.DotNet.Models
{
    public class JwtResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
    }
}
