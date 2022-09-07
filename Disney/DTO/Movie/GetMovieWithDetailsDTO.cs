using Disney.DTO.Character;
using Disney.DTO.Genre;
using System.ComponentModel.DataAnnotations;

namespace Disney.DTO.Movie
{
    public class GetMovieWithDetailsDTO
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public DateTime CreationDate { get; set; }

        public string Image { get; set; } = string.Empty;

        [Range(1, 5)]
        public int Rating { get; set; }

        public GetGenreDTO? Genre { get; set; }

        public IEnumerable<GetCharacterDTO> Characters { get; set; }
    }
}
