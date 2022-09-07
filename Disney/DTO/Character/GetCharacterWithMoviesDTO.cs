using Disney.DTO.CharacterMovie;
using Disney.DTO.Movie;

namespace Disney.DTO.Character
{
    public class GetCharacterWithMoviesDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }

        public int Weigth { get; set; }

        public string Image { get; set; } = string.Empty;

        public string Bio { get; set; } = string.Empty;

        public IEnumerable<GetMovieDTO>? Movies { get; set; }
    }
}
