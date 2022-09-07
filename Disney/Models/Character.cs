using System.Text.Json.Serialization;

namespace Disney.Models
{
    public class Character
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }

        public int Weigth { get; set; }

        public string Image { get; set; } = string.Empty;

        public string Bio { get; set; } = string.Empty;

        //Navigation Properties
        public IEnumerable<Movie> Movies { get; set; }

    }
}
