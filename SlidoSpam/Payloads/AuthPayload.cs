using Newtonsoft.Json;

namespace SlidoSpam.Payloads;

public class AuthPayload
{
    [JsonProperty("initialAppViewer")]
    public string InitialAppViewer { get; set; }

    [JsonProperty("granted_consents")]
    public string[] GrantedConsents { get; set; }
}