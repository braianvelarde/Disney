using System.ComponentModel.DataAnnotations;

namespace Disney.DTO.Auth
{
    public class LoginUserDTO
    {
        [Required]
        [MinLength(6)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;
    }
}
