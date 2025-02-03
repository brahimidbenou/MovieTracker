using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using BackEnd.Models;
using System.Text.Json; 
using Microsoft.Extensions.Configuration;

namespace BackEnd.Services
{
    public class OmdbService
    {
        private readonly HttpClient _httpClient;
        
        public OmdbService(HttpClient httpClient, IConfiguration conf) {
            _httpClient = httpClient;
        }

        public async Task<List<OmdbFilmDetails>> SearchByTitle(string title) {
           
            var response = await _httpClient.GetStringAsync($"https://www.omdbapi.com/?s={title}&apikey=f3f3640f");
            var searchResponse = JsonSerializer.Deserialize<OmdbSearchResponse>(response);
            var films = new List<OmdbFilmDetails>();
            if  (searchResponse != null) {
                foreach (var omdbFilm in searchResponse.Search) {
                    films.Add(new OmdbFilmDetails {
                        Title = omdbFilm.Title,
                        Year = omdbFilm.Year,
                        imdbID = omdbFilm.imdbID,
                        Poster = omdbFilm.Poster
                    });
                }
            }
            return films;
        }

        public async Task<OmdbFilmDetails?> GetByImdbId(string imdbId) {
            var response = await _httpClient.GetAsync($"https://www.omdbapi.com/?s={imdbId}apikey=f3f3640f");
            var searchResponse = JsonSerializer.Deserialize<OmdbFilmDetails>(await response.Content.ReadAsStringAsync());
            if (searchResponse != null) {
                return new OmdbFilmDetails {
                        Title = searchResponse.Title,
                        Year = searchResponse.Year,
                        imdbID = searchResponse.imdbID,
                        Poster = searchResponse.Poster
                    };
            }
            return null;
        }

    }
}