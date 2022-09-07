using AutoMapper;
using Disney.DTO.Genre;
using Disney.Models;

namespace Disney.Services.GenreService
{
    public class GenreService : IGenreService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GenreService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<List<GetGenreDTO>>> AddGenre(AddGenreDTO newGenre)
        {
            Genre genre = _mapper.Map<Genre>(newGenre);
            Response<List<GetGenreDTO>> response = new Response<List<GetGenreDTO>> ();
            try
            {
                _context.Add(genre);
                await _context.SaveChangesAsync();
                response.Data = await _context.Genres.Select(g=> _mapper.Map<GetGenreDTO>(g)).ToListAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response<List<GetGenreDTO>>> DeleteGenre(int id)
        {
            Response<List<GetGenreDTO>> response = new Response<List<GetGenreDTO>>();
            try
            {
                var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);
                if (genre != null)
                {
                    _context.Genres.Remove(genre);
                    await _context.SaveChangesAsync();
                    response.Data = await _context.Genres.Select(g=>_mapper.Map<GetGenreDTO>(g)).ToListAsync();
                }
                else
                {
                    response.Success = false;
                    response.Message = "Genre not found";
                }
                

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response<GetGenreDTO>> GetById(int id)
        {
            Response<GetGenreDTO> response = new Response<GetGenreDTO>();
            try
            {
                var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);
                if (genre != null)
                {
                    response.Data = _mapper.Map<GetGenreDTO>(genre);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Genre not found";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response<List<GetGenreDTO>>> GetGenres()
        {
            Response<List<GetGenreDTO>> response = new Response<List<GetGenreDTO>>();
            try
            {
                response.Data = await _context.Genres.Select(g => _mapper.Map<GetGenreDTO>(g)).ToListAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response<GetGenreDTO>> UpdateGenre(UpdateGenreDTO updatedGenre)
        {
            Response<GetGenreDTO> response = new Response<GetGenreDTO>();
            try
            {
                var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == updatedGenre.Id);
                if (genre!= null)
                {
                    genre.Name = updatedGenre.Name;
                    genre.Image = updatedGenre.Image;
                    await _context.SaveChangesAsync();
                    response.Data = _mapper.Map<GetGenreDTO>(genre);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Genre not found";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
