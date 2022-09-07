using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Disney.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public DateTime CreationDate { get; set; }

        public string Image { get; set; } = string.Empty;

        [Range(1,5)]
        public int Rating { get; set; }

        public Genre? Genre { get; set; }

        public int GenreId { get; set; }
        
        public IEnumerable<Character> Characters { get; set; }
    }
}
