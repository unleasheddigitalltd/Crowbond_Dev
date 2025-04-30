using Newtonsoft.Json;

namespace Crowbond.Modules.OMS.Application.Choco.Responses;

public class ChocoTokenResponse
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }
}
