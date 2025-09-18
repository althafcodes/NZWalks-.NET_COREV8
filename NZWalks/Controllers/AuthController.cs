using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.DTO;
using NZWalks.Repository;

namespace NZWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        //UserManager => comes from package installed
        //we also injected UserManager into the application
        //thats reason it is available 
        //it is useful to create or register new user

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        //post -> /api/auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDTO.Username,
                Email = registerRequestDTO.Username
            };

            //creating identity/ user
            var identityresult = await userManager.CreateAsync(identityUser, registerRequestDTO.Password);

            if (identityresult.Succeeded)
            {
                //add roles to user
                if(registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                {
                    identityresult = await userManager.AddToRolesAsync(identityUser, registerRequestDTO.Roles);
                }

                if (identityresult.Succeeded)
                {
                    return Ok("User registration is successfull!.");
                }
            }

            return BadRequest("Something went wrong !");
        }

        //post -> api/auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDTO.UserName);

            if (user == null)
            {
                return BadRequest("Invalid User!.");
            }

            var checkPwdResult = await userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

            if (checkPwdResult)
            {
                //Get roles for the input user
                var roles = await userManager.GetRolesAsync(user);

                if(roles != null)
                {

                    //create token here
                    var jwttoken = tokenRepository.CreateJWTToken(user, roles.ToList());
                    var response = new LoginResponseDTO
                    {
                        JWTToken = jwttoken
                    };
                    
                    return Ok(response);
                }


                return Ok();
            }
            else
            {
                return BadRequest("Invalid Password!.");
            }
        }
    }
}
