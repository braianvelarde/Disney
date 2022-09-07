using Disney.DTO.Movie;
using Disney.Models;

namespace Disney.Services.MovieService
{
    public interface IMovieService
    {
        Task<Response<List<GetMovieDTO>>> GetMovies();

        Task<Response<List<GetMovieWithDetailsDTO>>> GetMoviesByQuery(string? name, int? genreId, string? order);

        Task<Response<GetMovieWithDetailsDTO>> GetMovieDetails(int id);

        Task<Response<List<GetMovieDTO>>> AddMovie(AddMovieDTO newMovie);

        Task<Response<List<GetMovieDTO>>> UpdateMovie(UpdateMovieDTO updatedMovie);

        Task<Response<List<GetMovieDTO>>> DeleteMovie(int id);
    }
}
