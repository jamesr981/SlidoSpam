using Newtonsoft.Json;

namespace SlidoSpam.Payloads;

public class LikePayload
{
    [JsonProperty("score")]
    public int Score => Like ? 1 : 0;

    public bool Like { get; set; }
}