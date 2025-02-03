using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontEnd.Components.Shared;

namespace FrontEnd.Services
{
    public class AuthProvider : AuthenticationStateProvider
    {
        private readonly ProtectedLocalStorage _sessionStorage;

        public AuthProvider(ProtectedLocalStorage protectedSessionStorage)
        {
            _sessionStorage = protectedSessionStorage;
        }

        public async Task<int> GetIdAsync() 
        {
            var result = await _sessionStorage.GetAsync<string>("Token");
            if (result.Success && !string.IsNullOrEmpty(result.Value))
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken(result.Value); 
                var idClaim = jsonToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
                Console.WriteLine(idClaim.ToString());
                int id = Int32.Parse(idClaim);
                return id;
            }

            return 0; 
        }

        public async Task<string> GetAdmin() 
        {
            var result = await _sessionStorage.GetAsync<string>("Token");
            if (result.Success && !string.IsNullOrEmpty(result.Value))
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken(result.Value); 
                var idClaim = jsonToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                Console.WriteLine(idClaim.ToString());
                return idClaim;
            }

            return ""; 
        }

        public async Task Login(User user, string token)
        {
            try
            {
                Console.WriteLine(token);
                await _sessionStorage.SetAsync("User", user); // Stocke l'utilisateur
                await _sessionStorage.SetAsync("Token", token); // Stocke le token
                ClaimsPrincipal claim = GenerateClaimsPrincipal(user); // Génère un ClaimsPrincipal
                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claim))); // Notifie le changement d'état de connexion
            }
            catch(Exception ex)
            {
                Console.WriteLine("erreur de connexion");
            }
            
        }

        public ClaimsPrincipal GenerateClaimsPrincipal(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Nom),
                new Claim(ClaimTypes.Role, user.RoleType.ToString())
            };
            ClaimsIdentity identity = new ClaimsIdentity(claims, "custom");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
            return claimsPrincipal;
        }

        public async Task Logout()
        {
            await _sessionStorage.DeleteAsync("User"); // Supprime l'utilisateur
            await _sessionStorage.DeleteAsync("Token"); // Supprime le token
            var claimDisconnected = new ClaimsPrincipal(new ClaimsIdentity()); // Créer un objet ClaimsPrincipal vide == utilisateur non connecté
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimDisconnected))); // Notifie le changement d'état de connexion
        }
        
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var userResult = await _sessionStorage.GetAsync<User>("User");
                if (userResult.Value != null)
                {
                    var claim = GenerateClaimsPrincipal(userResult.Value);
                    return new AuthenticationState(claim);
                }
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("statically rendered"))
            {
                Console.WriteLine("erreur");
            }
            
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        

    }
}