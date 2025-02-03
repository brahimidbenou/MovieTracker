using System;
using FrontEnd.Components.Shared;
using System.Net.Http.Json;

namespace FrontEnd.Services
{
    public class GetFilms
    {
        private readonly HttpClient _httpClient;
        
        public GetFilms(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Film>> GetAllFilms()
        {
            return await _httpClient.GetFromJsonAsync<List<Film>>("api/Films/AllFilms");
        }

        public async Task<bool> AddFilm(OmdbFilmDetails omdbFilm) 
        {
            Film film = new Film
            {
                Title = omdbFilm.Title,
                Poster = omdbFilm.Poster,
                Imdb = omdbFilm.imdbID,
                Year = Int32.Parse(omdbFilm.Year)
            };

            var response = await _httpClient.PostAsJsonAsync("api/Films/add", film); 

            return response.IsSuccessStatusCode;
        }

    }
}