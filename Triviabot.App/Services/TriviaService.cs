using System.Text.Json;
using Triviabot.App.Models;

namespace Triviabot.App.Services;

public class TriviaService
{
    private readonly HttpClient _httpClient;

    public TriviaService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<TriviaModel> Get(int category)
    {
       var result =  await _httpClient.GetAsync($"https://opentdb.com/api.php?amount=1&category={category}&type=multiple");

       if (!result.IsSuccessStatusCode)
       {
           return null;
       }
       
       var json = await result.Content.ReadAsStringAsync();
       var root = JsonSerializer.Deserialize<TriviaModel>(json);

       return root;
    }
}