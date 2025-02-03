using Microsoft.AspNetCore.Mvc;
using BackEnd.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using BackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
namespace BackEnd.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext _userManager;
        private readonly JwtService _jwtService;
        public UserController(UserContext userManager, JwtService jwtService) {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<User>> GetUser(int id) {
            var user = await _userManager.Users.FindAsync(id);
            if (user == null){
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<UserLoginResponse>> PostLogin(Userinfo loginUser)
        {
            var userInDb = await _userManager.Users
                .FirstOrDefaultAsync(u => u.Nom == loginUser.Nom);
            if (userInDb == null)
            {
                return NotFound("Utilisateur non trouvé.");
            }

            var hasher = new PasswordHasher<User>();
            var verificationResult = hasher.VerifyHashedPassword(userInDb, userInDb.MotDePasse, loginUser.MotDePasse);
            if (verificationResult == PasswordVerificationResult.Success)
            {
                var userRole = userInDb.RoleType;
                var token = _jwtService.GenerateToken(
                    userInDb.Id.ToString(),
                    userInDb.Nom,
                    userRole.ToString()
                );

                return Ok(new UserLoginResponse 
                { 
                    Message = "Login successful, Welcome back", 
                    User = userInDb, 
                    Jeton = token 
                });
            }
            else
            {
                return Unauthorized("Mot de passe invalide.");
            }
        }

        // GET /api/User 
        [HttpGet("AllUsers")]
        [AllowAnonymous] 
        public List<User> GetallUsers()
        {
            List<User> UsersNom =  _userManager.Users
                                            .Select(u => u)
                                            .ToList();

            return UsersNom;
        }


        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<ActionResult<Userinfo>> PostRegister(Userinfo u) {
            var hasher = new PasswordHasher<Userinfo>();
            var password = u.MotDePasse;
            u.MotDePasse = hasher.HashPassword(u, password);
            User uDB = new User()
            {
                Nom = u.Nom,
                MotDePasse = u.MotDePasse
            };
            _userManager.Users.Add(uDB);
            await _userManager.SaveChangesAsync();
            return Ok(u);
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> PutId(int id, string nom, string password, User.Role role) {
            var user = await _userManager.Users.FindAsync(id);
            if (user == null) {
                return NotFound();
            }
            user.Nom = nom;
            var hasher = new PasswordHasher<User>();
            user.MotDePasse = hasher.HashPassword(user, password);
            user.RoleType = role;
            await _userManager.SaveChangesAsync();
            return Ok("success");
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userManager.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _userManager.Users.Remove(user);

            await _userManager.SaveChangesAsync();

            return Ok("Deleted");
        }

    }
}