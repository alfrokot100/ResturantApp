using ResturantMVC.Models;
using System.Net.Http.Headers;

namespace ResturantMVC.Services
{
    public class MenuApiService
    {
        private readonly HttpClient _httpClient;

        public MenuApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private void AddJwtHeader(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
        }
        public async Task<List<DishViewModel>> GetPopularDishesAsync()
        {
            var dishes = await _httpClient.GetFromJsonAsync<List<DishViewModel>>("api/Menu/popular");
            return dishes ?? new List<DishViewModel>();
        }

        public async Task<List<DishViewModel>> GetAllDishesAsync(string? token = null)
        {
            if (!string.IsNullOrEmpty(token))
                AddJwtHeader(token);

            var dishes = await _httpClient.GetFromJsonAsync<List<DishViewModel>>("api/Menu");
            return dishes ?? new List<DishViewModel>();
        }

        public async Task<DishViewModel?> GetDishByIdAsync(int id, string token)
        {
            AddJwtHeader(token);
            return await _httpClient.GetFromJsonAsync<DishViewModel>($"api/Menu/{id}");
        }

        public async Task<bool> CreateDishAsync(DishViewModel dish, string token)
        {
            AddJwtHeader(token);
            var respone = await _httpClient.PostAsJsonAsync("api/Menu", dish);
            return respone.IsSuccessStatusCode;
        }

        //Uppdaterar menyn på hemsidan för admin
        public async Task<bool> UpdateDishAsync(DishViewModel dish, string token)
        {
            AddJwtHeader(token);
            var response = await _httpClient.PutAsJsonAsync($"api/Menu/{dish.Id}", dish);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteDishAsync(string token, int id)
        {
            AddJwtHeader(token);
            var response = await _httpClient.DeleteAsync($"api/Menu/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
