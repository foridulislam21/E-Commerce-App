using System.Threading.Tasks;
using API.Configurations.Error;
using API.Models.DTO;
using API.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController (UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost ("login")]
        public async Task<ActionResult<UserDto>> Login (LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync (loginDto.Email);
            if (user == null)
            {
                return Unauthorized (new ApiResponse (401));
            }
            var result = await _signInManager.CheckPasswordSignInAsync (user, loginDto.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized (new ApiResponse (401));
            }
            return new UserDto
            {
                Email = user.Email,
                    Token = "Token Info",
                    DisplayName = user.DisplayName
            };
        }

        [HttpPost ("register")]
        public async Task<ActionResult<UserDto>> Register (RegisterDto registerDto)
        {
            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email
            };
            var result = await _userManager.CreateAsync (user, registerDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest (new ApiResponse (400));
            }
            return new UserDto
            {
                Email = user.Email,
                    Token = "Token Info",
                    DisplayName = user.DisplayName
            };
        }
    }
}