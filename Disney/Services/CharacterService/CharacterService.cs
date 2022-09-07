using AutoMapper;
using Disney.DTO.Character;
using Disney.DTO.CharacterMovie;
using Disney.DTO.Movie;
using Disney.Models;

namespace Disney.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CharacterService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<GetCharacterDTO>> Add(AddCharacterDTO newCharacter)
        {
            Character character = _mapper.Map<Character>(newCharacter);
            Response<GetCharacterDTO> serviceResponse = new Response<GetCharacterDTO>();
            try
            {
                _context.Characters.Add(character);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetCharacterDTO>(character);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<Response<List<GetCharacterDTO>>> Delete(int id)
        {
            Response<List<GetCharacterDTO>> serverReponse = new Response<List<GetCharacterDTO>>();
            try
            {
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
                if (character == null)
                {
                    serverReponse.Success = false;
                    serverReponse.Message = "Character not found";
                }
                else
                {
                    _context.Characters.Remove(character);
                    await _context.SaveChangesAsync();
                    serverReponse.Data = await _context.Characters
                            .Select(c => _mapper.Map<GetCharacterDTO>(c))
                            .ToListAsync();
                }  
                return serverReponse;
            }
            catch (Exception ex)
            {
                serverReponse.Success = false;
                serverReponse.Message = "There is an error " + ex.Message;
                return serverReponse;
            }
        }

        public async Task<Response<GetCharacterWithMoviesDTO>> GetCharacterById(int id)
        {
            Response<GetCharacterWithMoviesDTO> serviceResponse = new Response<GetCharacterWithMoviesDTO>();
            try
            {
                var character = await _context.Characters.Include(c=>c.Movies).FirstOrDefaultAsync(c => c.Id == id);
                if (character != null)
                {
                    serviceResponse.Data = new GetCharacterWithMoviesDTO
                    {
                        Age = character.Age,
                        Bio = character.Bio,
                        Id = character.Id,
                        Image = character.Image,
                        Name = character.Name,
                        Weigth = character.Weigth,
                        Movies = character.Movies.Select(m => _mapper.Map<GetMovieDTO>(m)).ToList()
                    };
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
            
        }

        public async Task<Response<List<GetCharacterDTO>>> GetCharacters()
        {
            Response<List<GetCharacterDTO>> serviceResponse = new Response<List<GetCharacterDTO>>
            {
                Data = await _context.Characters.Select(c=>_mapper.Map<GetCharacterDTO>(c))
                .ToListAsync()
        };
            return serviceResponse;
        }

        

        public async Task<Response<List<GetCharacterWithMoviesDTO>>> GetCharactersByQuery(string? name, int? age, int? movieId)
        {
            Response<List<GetCharacterWithMoviesDTO>> serviceResponse = new Response<List<GetCharacterWithMoviesDTO>>();
            try
            {
                if(name == null && age == null && movieId == null)
                {
                    serviceResponse.Data = await _context.Characters
                        .Include(c=>c.Movies)
                        .Select(c => _mapper.Map<GetCharacterWithMoviesDTO>(c)).ToListAsync();

                }
                else
                {
                    serviceResponse.Data = await _context.Characters
                   .Include(c=>c.Movies)
                   .Where(c => c.Name.Contains(name) || c.Age == age || c.Movies.Any(m => m.Id == movieId))
                   .Select(c => _mapper.Map<GetCharacterWithMoviesDTO>(c))
                   .ToListAsync();
                }             
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<Response<GetCharacterDTO>> Update(UpdateCharacterDTO updatedCharacter)
        {
            Response<GetCharacterDTO> serviceResponse = new Response<GetCharacterDTO>();
            try
            {
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
                if (character == null)
                {
                    serviceResponse.Message = "Character not found";
                }
                else
                {
                    character.Name = updatedCharacter.Name;
                    character.Age = updatedCharacter.Age;
                    character.Weigth = updatedCharacter.Weigth;
                    character.Bio = updatedCharacter.Bio;
                    character.Image = updatedCharacter.Image;
                    serviceResponse.Data = _mapper.Map<GetCharacterDTO>(character);
                }

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}
