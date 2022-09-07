using Disney.DTO.Genre;
using Disney.Models;
using Disney.Services.GenreService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Disney.Controllers
{
    [Route("api/genres")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;
        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetGenreDTO>>> GetGenres()
        {
            try
            {
                return Ok(await _genreService.GetGenres());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<List<GetGenreDTO>>> DeleteGenre(int id)
        {
            try
            {
                return Ok(await _genreService.DeleteGenre(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<GetGenreDTO>>> AddGenre(AddGenreDTO genre)
        {
            try
            {
                return Ok(await _genreService.AddGenre(genre));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<List<GetGenreDTO>>> UpdateGenre(UpdateGenreDTO updatedGenre)
        {
            try
            {
                Response<GetGenreDTO> response = new Response<GetGenreDTO>();
                response = await _genreService.UpdateGenre(updatedGenre);
                if(response.Data == null)
                {
                    return NotFound(response);
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
