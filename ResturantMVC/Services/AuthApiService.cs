using ResturantMVC.Models;

namespace ResturantMVC.Services
{
    public class AuthApiService 
    {
        private readonly HttpClient _httpClient;

        public AuthApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> LoginAsync(LoginViewModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/AuthControllerFirst/login", model);
            
            //Felsökning
            var requestJson = System.Text.Json.JsonSerializer.Serialize(model);

            if (!response.IsSuccessStatusCode) { return null; }

            var json = await response.Content.ReadAsStringAsync();

            //var data = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();

            // Försök läsa in som dictionary
            var data = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(
                json,
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            if (data != null && data.ContainsKey("token"))
            {
                return data["token"];
            }

            return null;
        }
    }
}
