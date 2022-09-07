using Disney.DTO.Character;
using Disney.Models;
using Disney.Services.CharacterService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Disney.Controllers
{
    [Route("api/characters")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetCharacterDTO>>> GetCharacters([FromQuery] string? name, int? age, int? movieId)
        {
            try
            {
                return Ok(await _characterService.GetCharactersByQuery(name, age, movieId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<GetCharacterDTO>>> GetCharacterById(int id)
        {
            try
            {
                return Ok(await _characterService.GetCharacterById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<List<GetCharacterDTO>>> DeleteCharacter(int id)
        {
            Response<List<GetCharacterDTO>> response = new Response<List<GetCharacterDTO>>();
            try
            {
                response = await _characterService.Delete(id);
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

        [HttpPost]
        public async Task<ActionResult<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO newCharacter)
        {
            try
            {
                return Ok(await _characterService.Add(newCharacter));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<List<GetCharacterDTO>>> UpdateCharacter(UpdateCharacterDTO updatedCharacter)
        {
            Response<GetCharacterDTO> response = new Response<GetCharacterDTO>();
            try
            {
                response = await _characterService.Update(updatedCharacter);
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
