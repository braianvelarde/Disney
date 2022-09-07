using Disney.DTO.Movie;
using Disney.Models;
using Disney.Services.MovieService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Disney.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetMovieWithDetailsDTO>>> GetAll([FromQuery] string? name, int? genreId, string? order)
        {
            Response<List<GetMovieWithDetailsDTO>> response = new Response<List<GetMovieWithDetailsDTO>>();
            try
            {
                response = await _movieService.GetMoviesByQuery(name, genreId, order);
                if (response.Data == null)
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

        [HttpGet("{id}")]
        public async Task<ActionResult<GetMovieWithDetailsDTO>> GetWithDetails(int id)
        {
            Response<GetMovieWithDetailsDTO> response = new Response<GetMovieWithDetailsDTO>();
            try
            {
                response = await _movieService.GetMovieDetails(id);
                if (response.Data == null)
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

        [HttpDelete]
        public async Task<ActionResult<List<GetMovieDTO>>> Delete(int id)
        {
            Response<List<GetMovieDTO>> response = new Response<List<GetMovieDTO>>();
            try
            {
                response = await _movieService.DeleteMovie(id);
                if (response.Data == null)
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
