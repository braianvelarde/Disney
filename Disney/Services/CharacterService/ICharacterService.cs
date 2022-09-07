using Disney.DTO.Character;
using Disney.Models;

namespace Disney.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<Response<List<GetCharacterDTO>>> GetCharacters();

        Task<Response<List<GetCharacterWithMoviesDTO>>> GetCharactersByQuery(string? name, int? age, int? movieId);

        Task<Response<GetCharacterWithMoviesDTO>> GetCharacterById(int id);

        Task<Response<GetCharacterDTO>> Add(AddCharacterDTO newCharacter);

        Task<Response<GetCharacterDTO>> Update(UpdateCharacterDTO updatedCharacter);

        Task<Response<List<GetCharacterDTO>>> Delete(int id);

    }
}
