using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEnd.Models;
using BackEnd.Services;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OmdbController : ControllerBase
    {
        private readonly OmdbService _userManager;

        public OmdbController(OmdbService userManager) {
            _userManager = userManager;
        }

        [HttpGet("search/{title}")]
        public async Task<ActionResult<OmdbFilmDetails>> search(string title) {
            var res = await _userManager.SearchByTitle(title);
            if (res == null || res.Count() == 0) {
                return NotFound("nothing!!");
            }
            return Ok(res);
        }

        [HttpGet("import/{imdbId}")]
        public async Task<ActionResult<OmdbFilmDetails>> import(string imdbId) {
            var res = await _userManager.GetByImdbId(imdbId);
            if (res == null) {
                return NotFound("nothing!!");
            }
            return Ok(res);
        }
    }
}
