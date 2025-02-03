using System;
using FrontEnd.Components.Shared;
using System.Net.Http.Json;

namespace FrontEnd.Services
{
    public class FavoritesService
    {
        private readonly HttpClient _httpClient;
        
        public FavoritesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Film>> GetFavorites(int userId)
        {
            var favoritesUri = $"api/Favorites/list/{userId}";
            
            var favorites = await _httpClient.GetFromJsonAsync<List<Favorites>>(favoritesUri);
            var allFilms = await _httpClient.GetFromJsonAsync<List<Film>>("api/Films/AllFilms");

            if (favorites == null || allFilms == null)
            {
                return new List<Film>();
            }
            
            var favoriteFilmIds = favorites.Select(fav => fav.FilmId).ToHashSet();
            var favoriteFilms = allFilms.Where(film => favoriteFilmIds.Contains(film.Id)).ToList();
            
            return favoriteFilms;
        }

        public async Task<bool> AddToFavorites(int filmID, int userId)
        {
            var favorite = new Favorites{
                UserId = userId, 
                FilmId = filmID
            };
            Console.WriteLine(favorite.UserId.ToString());
            Console.WriteLine(favorite.FilmId.ToString());
            var response = await _httpClient.PostAsJsonAsync("api/Favorites/add", favorite);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteFavorites(int id, int userId){
            bool success = false;
            var favoritesUri = $"api/Favorites/list/{userId}";
            var favorites = await _httpClient.GetFromJsonAsync<List<Favorites>>(favoritesUri);
            foreach (var film in favorites) {
                if (film.FilmId == id) {
                    var res = await _httpClient.DeleteAsync($"api/Favorites/Remove/{film.Id}");
                    if(res.IsSuccessStatusCode) success = true;
                }
            }
            return success;
        }

    }
}