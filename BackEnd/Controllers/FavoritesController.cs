using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEnd.Models;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly FavorisContext _userManager;

        public FavoritesController(FavorisContext userManager) {
            _userManager = userManager;
        }

        [HttpPost("add")]
        public async Task<ActionResult<Favorites>> PostRegister(Favorites f) {
            bool favoriteExists = await _userManager.Favoris
                .AnyAsync(x => x.UserId == f.UserId && x.FilmId == f.FilmId);

            if (favoriteExists)
            {
                return Conflict("Ce film est déjà dans les favoris de cet utilisateur");
            }
            _userManager.Favoris.Add(f);
            await _userManager.SaveChangesAsync();
            return Ok("created!!");
        }

        [HttpDelete("Remove/{id}")]
        public async Task<IActionResult> DeleteFavorites(int id) {
            var f = await _userManager.Favoris.FindAsync(id);
            if (f == null) {
                return NotFound("No film!");
            }
            _userManager.Favoris.Remove(f);
            await _userManager.SaveChangesAsync();
            return Ok("Deleted!");
        }

        [HttpGet("list/{userId}")]
        public async Task<ActionResult> GetList(int userId) {
            var arr = await _userManager.Favoris
            .Where(f => f.UserId == userId)
            .Select(f => f).ToListAsync();
            if (arr == null || arr.Count() == 0) {
                return NotFound("Nothing!");
            } 
            return Ok(arr);
        }

        [HttpGet("AllFavorites")]
        public async Task<ActionResult<Favorites>> GetAllFavorites() {
            var film = await _userManager.Favoris.ToListAsync();
            if (film == null || film.Count == 0) {
                return NotFound("nothing");
            }
            return Ok(film);
        }

    }
}
