using Examen_PF.MVVM.Models;
using Newtonsoft.Json;

namespace Examen_PF.Services;

public class APIService
{
    public HttpClient _Client { get; set; }
    
    public APIService()
    {
        _Client = new HttpClient();
    }
    
    //Usar Api https://api.chucknorris.io/
    public async Task<string> GetRandomJoke()
    {
        var response = await _Client.GetAsync("https://api.chucknorris.io/jokes/random");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var joke = JsonConvert.DeserializeObject<ModelJoke>(content);

        return joke.Value;
    }
}