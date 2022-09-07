using AutoMapper;
using Disney.DTO.Character;
using Disney.DTO.Movie;
using Disney.Models;

namespace Disney.Services.MovieService
{
    public class MovieService : IMovieService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public MovieService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<List<GetMovieDTO>>> AddMovie(AddMovieDTO newMovie)
        {
            Movie movie = _mapper.Map<Movie>(newMovie);
            Response<List<GetMovieDTO>> response = new Response<List<GetMovieDTO>>();
            try
            {
                _context.Movies.Add(movie);
                await _context.SaveChangesAsync();
                response.Data = await _context.Movies
                    .Select(m=> _mapper.Map<GetMovieDTO>(m))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response<List<GetMovieDTO>>> DeleteMovie(int id)
        {
            Response<List<GetMovieDTO>> response = new Response<List<GetMovieDTO>>();
            try
            {
                var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
                if (movie == null)
                {
                    response.Success = false;
                    response.Message = "Movie not found";
                }
                else
                {
                    _context.Movies.Remove(movie);
                    await _context.SaveChangesAsync();
                    response.Data = await _context.Movies
                    .Select(m => _mapper.Map<GetMovieDTO>(m))
                    .ToListAsync();
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response<GetMovieWithDetailsDTO>> GetMovieDetails(int id)
        {
            Response<GetMovieWithDetailsDTO> response = new Response<GetMovieWithDetailsDTO>();
            try
            {
                var movie = await _context.Movies
                    .Include(m=>m.Characters)                   
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (movie== null)
                {
                    response.Success = false;
                    response.Message = "Movie not found";
                }
                else
                {
                    response.Data = _mapper.Map<GetMovieWithDetailsDTO>(movie);
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response<List<GetMovieDTO>>> GetMovies()
        {
            Response<List<GetMovieDTO>> response = new Response<List<GetMovieDTO>>();
            try
            {
                response.Data = await _context.Movies.Select(m => _mapper.Map<GetMovieDTO>(m)).ToListAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response<List<GetMovieWithDetailsDTO>>> GetMoviesByQuery(string? name, int? genreId, string? orderBy)
        {
            Response<List<GetMovieWithDetailsDTO>> response = new Response<List<GetMovieWithDetailsDTO>>();
            try
            {
                if (name == null && genreId == null)
                {
                    response.Data = await _context.Movies
                        .Include(m => m.Characters)
                        .Include(m=>m.Genre)
                        .Select(m => _mapper.Map<GetMovieWithDetailsDTO>(m))
                        .ToListAsync();
                }
                else
                {
                    response.Data = await _context.Movies
                        .Include(m => m.Characters)
                        .Include(m => m.Genre)
                        .Where(m => m.Title.Contains(name) || m.GenreId == genreId)
                        .Select(m => _mapper.Map<GetMovieWithDetailsDTO>(m))
                        .ToListAsync();
                }
                if (orderBy == "DESC")
                {
                    response.Data = response.Data.OrderByDescending(m => m.CreationDate).ToList();
                }
                if(orderBy == "ASC")
                {
                    response.Data = response.Data.OrderBy(m => m.CreationDate).ToList();
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public Task<Response<List<GetMovieDTO>>> UpdateMovie(UpdateMovieDTO updatedMovie)
        {
            throw new NotImplementedException();
        }
    }
}
