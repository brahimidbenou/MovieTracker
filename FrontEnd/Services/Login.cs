using System;
using System.Net.Http.Json;
using FrontEnd.Components.Shared;

namespace FrontEnd.Services
{
    public class Login
    {
        private readonly HttpClient _httpClient;

        public Login(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }

        public async Task<UserLoginResponse?> SignIn(Userinfo u) {
            var res = await _httpClient.PostAsJsonAsync("api/User/login", u);
            Console.WriteLine(res.IsSuccessStatusCode.ToString());
            if (res.IsSuccessStatusCode)
            {
                return await res.Content.ReadFromJsonAsync<UserLoginResponse>();
            }
            return null;
        }
    }
}