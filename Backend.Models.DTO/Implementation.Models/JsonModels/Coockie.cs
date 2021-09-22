using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Models.ImplementationModels.JsonModels
{
    public class Cookie
    {
        public class CoockieModel
        {
            [JsonPropertyName("domain")]
            public string Domain { get; set; }

            [JsonPropertyName("expires")]
            public float Expires { get; set; }

            [JsonPropertyName("httpOnly")]
            public bool HttpOnly { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("path")]
            public string Path { get; set; }

            [JsonPropertyName("priority")]
            public string Priority { get; set; }

            [JsonPropertyName("secure")]
            public bool Secure { get; set; }

            [JsonPropertyName("session")]
            public bool Session { get; set; }

            [JsonPropertyName("size")]
            public int Size { get; set; }

            [JsonPropertyName("value")]
            public string Value { get; set; }

            [JsonPropertyName("sameSite")]
            public string SameSite { get; set; }
        }

        public class CoockieRootModel
        {
            [JsonPropertyName("cookies")]
            public List<CoockieModel> Cookies { get; set; }
        }
    }
}
