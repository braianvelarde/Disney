namespace Disney.DTO.Movie
{
    public class AddMovieDTO
    {

        public string Title { get; set; } = string.Empty;

        public DateTime CreationDate { get; set; }

        public string Image { get; set; } = string.Empty;

        public int Rating { get; set; }

        public int GenreId { get; set; }
    }
}
