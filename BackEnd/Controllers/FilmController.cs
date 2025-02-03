using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEnd.Models;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        private readonly FilmContext _userManager;

        public FilmsController(FilmContext userManager) {
            _userManager = userManager;
        }

        [HttpPost("add")]
        public async Task<ActionResult<Film>> PostRegister(Film f) {
            bool filmExists = await _userManager.Films
                .AnyAsync(x => x.Imdb == f.Imdb);

            if (filmExists)
            {
                return Conflict("Ce film existe déjà dans la base de données");
            }
            _userManager.Films.Add(f);
            await _userManager.SaveChangesAsync();
            return Ok("created!!");
        }
        
        [HttpGet("AllFilms")]
        public async Task<ActionResult<Film>> GetFilm() {
            var film = await _userManager.Films.ToListAsync();
            if (film == null || film.Count == 0) {
                return NotFound("nothing");
            }
            return Ok(film);
        }

        [HttpGet("Search")]
        public async Task<ActionResult<Film>> Search(string s) {
            var film = await _userManager.Films.FirstOrDefaultAsync(f => f.Title.Contains(s));
            if (film == null) {
                return NotFound("nothing");
            }
            return Ok(film);
        }

        [HttpGet("info")]
        public async Task<ActionResult<Film>> getInfo(List<int> ids) {
            var film = await _userManager.Films.Where(f => ids.Contains(f.Id)).ToListAsync();
            if (film == null) {
                return NotFound("nothing");
            }
            return Ok(film);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilm(int id) {
            var film = await _userManager.Films.FindAsync(id);
            if (film == null) {
                return NotFound();
            } 
            _userManager.Films.Remove(film);
            await _userManager.SaveChangesAsync();
            
            return Ok("Deleted");
        }
    }
}
