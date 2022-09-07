using Disney.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Disney.DTO.Movie
{
    public class UpdateMovieDTO
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public DateTime CreationDate { get; set; }

        public string Image { get; set; } = string.Empty;

        [Range(1, 5)]
        public int Rating { get; set; }

        public int GenreId { get; set; }
    }
}
