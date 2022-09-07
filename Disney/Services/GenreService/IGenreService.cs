using Disney.DTO.Genre;
using Disney.Models;

namespace Disney.Services.GenreService
{
    public interface IGenreService
    {
        Task<Response<List<GetGenreDTO>>> GetGenres();

        Task<Response<GetGenreDTO>> GetById(int id);

        Task<Response<List<GetGenreDTO>>> AddGenre(AddGenreDTO newGenre);

        Task<Response<GetGenreDTO>> UpdateGenre(UpdateGenreDTO updatedGenre);

        Task<Response<List<GetGenreDTO>>> DeleteGenre(int id);
    }
}
