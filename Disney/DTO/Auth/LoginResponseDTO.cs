namespace Disney.DTO.Auth
{
    public class LoginResponseDTO
    {
        public string Token { get; set; } = string.Empty;

        public DateTime ValidTo { get; set; }

    }
}
