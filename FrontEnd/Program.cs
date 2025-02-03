using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using FrontEnd.Components;
using FrontEnd.Services;
using FrontEnd.Components.Shared;

namespace FrontEnd;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddHttpClient();
        
        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddScoped<SignUp>();
        builder.Services.AddScoped<Login>();
        builder.Services.AddScoped<ListUsers>();
        builder.Services.AddScoped<GetFilms>();
        builder.Services.AddScoped<FavoritesService>();
        builder.Services.AddScoped<OmdbService>();
        builder.Services.AddScoped<AuthProvider>();
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["ApiBaseAddress"]) });
        builder.Services.AddScoped<AuthenticationStateProvider, AuthProvider>();

        builder.Services.AddAuthentication(o =>
            {
            o.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
            options.Cookie.Name = "auth_cookie";
            options.LoginPath = "/Login"; // Redirection vers la page de connexion
            });

        builder.Services.AddAuthenticationCore();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
        }

        app.UseStaticFiles();
        app.UseAntiforgery();
        app.UseAuthentication();
        app.UseAuthorization();


        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}
