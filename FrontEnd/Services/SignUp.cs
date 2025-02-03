using System.Net.Http.Json; 
using FrontEnd.Components.Shared; 

namespace FrontEnd.Services
{
    public class SignUp 
    {
        private readonly HttpClient _httpClient;

        public SignUp(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }

        public async Task<Userinfo?> SignUpAsync(Userinfo u) 
        {
            var res = await _httpClient.PostAsJsonAsync("api/User/Register", u);
            if (res.IsSuccessStatusCode) 
            {
                return await res.Content.ReadFromJsonAsync<Userinfo>();
            }
            return null;
        }
    }
}
