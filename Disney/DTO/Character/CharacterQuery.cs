using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Disney.DTO.Character
{
    public class CharacterQuery
    {
        [FromQuery(Name="movies=idMovie")]
        public int MovieId { get; set; }

        [FromQuery(Name="name")]
        public string Name { get; set; } = string.Empty;

        [FromQuery(Name="age")]
        public int Age { get; set; }

    }
}
