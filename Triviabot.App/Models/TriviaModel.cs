using System.Text.Json.Serialization;

namespace Triviabot.App.Models;

public class TriviaModel
{
    [JsonPropertyName("response_code")] public int ResponseCode { get; set; }

    [JsonPropertyName("results")] public List<ResultModel> Results { get; set; }
}