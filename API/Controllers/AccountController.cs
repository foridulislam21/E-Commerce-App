using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Abstractions.TokenService;
using API.Configurations.Error;
using API.Configurations.UserExtentions;
using API.Models.DTO;
using API.Models.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _token;
        private readonly IMapper _mapper;
        public AccountController (UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService token, IMapper mapper)
        {
            _mapper = mapper;
            _token = token;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser ()
        {

            var user = await _userManager.FindByEmailFromClaimPrincipale (HttpContext.User);
            return new UserDto
            {
                DisplayName = user.DisplayName,
                    Email = user.Email,
                    Token = _token.CreateToken (user)
            };
        }

        [HttpGet ("emailExists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync ([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync (email) != null;
        }

        [Authorize]
        [HttpGet ("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress ()
        {
            var user = await _userManager.FindByEmailWithAddressAsync (HttpContext.User);
            return _mapper.Map<Address, AddressDto> (user.Address);
        }

        [Authorize]
        [HttpPut ("address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress (AddressDto addressDto)
        {
            var user = await _userManager.FindByEmailWithAddressAsync (HttpContext.User);
            user.Address = _mapper.Map<AddressDto, Address> (addressDto);
            var result = await _userManager.UpdateAsync (user);
            if (result.Succeeded)
            {
                return Ok (_mapper.Map<Address, AddressDto> (user.Address));
            }
            return BadRequest ("Updating Failed");
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
                    Token = _token.CreateToken (user),
                    DisplayName = user.DisplayName
            };
        }

        [HttpPost ("register")]
        public async Task<ActionResult<UserDto>> Register (RegisterDto registerDto)
        {
            if (CheckEmailExistsAsync (registerDto.Email).Result.Value)
            {
                return new BadRequestObjectResult (new ApiValidationErrorResponse { Errors = new [] { "Email address already in used!" } });
            }
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
                    Token = _token.CreateToken (user),
                    DisplayName = user.DisplayName
            };
        }
    }
}