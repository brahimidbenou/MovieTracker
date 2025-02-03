using System;
using FrontEnd.Components.Shared;
using System.Net.Http.Json;

namespace FrontEnd.Services
{
    public class ListUsers
    {
        private readonly HttpClient _httpClient;

        public ListUsers(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }
        public async Task<List<User>> GetList() 
        {
            return await _httpClient.GetFromJsonAsync<List<User>>("api/User/AllUsers");
        }
    }
}