namespace Disney.DTO.Character
{
    public class AddCharacterDTO
    {
        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }

        public int Weigth { get; set; }

        public string Image { get; set; } = string.Empty;

        public string Bio { get; set; } = string.Empty;

    }
}
