using System;
using FrontEnd.Components.Shared;
using System.Net.Http.Json;

namespace FrontEnd.Services
{
    public class OmdbService
    {
        private readonly HttpClient _httpClient;
        
        public OmdbService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<OmdbFilmDetails>> SearchFilm(string title) {
            return await _httpClient.GetFromJsonAsync<List<OmdbFilmDetails>>($"api/Omdb/search/{title}");
        }
    }
}