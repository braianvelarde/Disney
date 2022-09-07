using Disney.DTO.Auth;
using Disney.Models;
using Disney.Services.MailService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Disney.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ISendEmail _emailSender;

        public AuthController(
            UserManager<User> userManager, 
            SignInManager<User> signInManager,
            ISendEmail emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register(RegisterUserDTO request)
        {
            var userExists = await _userManager.FindByNameAsync(request.Username);
            if (userExists!= null)
            {
                return BadRequest();
            }
            var user = new User
            {
                UserName = request.Username,
                Email = request.Email,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                return BadRequest($"User creation failed. Errors: " + string.Join(", ", result.Errors.Select(x=>x.Description)));
            }
            await _emailSender.SendEmail(user.Email, user.UserName);
            return Ok("User created succesfully");
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(LoginUserDTO request)
        {
            var result = await _signInManager.PasswordSignInAsync(request.Username, request.Password, false, false);
            if (result.Succeeded)
            {
                var currentUser = await _userManager.FindByNameAsync(request.Username);

                if (currentUser.IsActive)
                {
                    return Ok(await GetToken(currentUser));
                }
            }
            return Unauthorized();
        }

        private async Task<LoginResponseDTO> GetToken(User currentUser)
        {
            var userRoles = await _userManager.GetRolesAsync(currentUser);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, currentUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            authClaims.AddRange(userRoles.Select(x => new Claim(ClaimTypes.Role, x)));
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KeySecretaParaAutenticacionJWTBearer"));
            var token =  new JwtSecurityToken(
                issuer: "https://localhost:7003", 
                audience: "https://localhost:7003", 
                expires: DateTime.Now.AddDays(1), 
                claims: authClaims, 
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));
            return new LoginResponseDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ValidTo = token.ValidTo
            };
        }
    }
}
