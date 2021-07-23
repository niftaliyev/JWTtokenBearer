using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JWTTest.Models;
using JWTTest.Models.DTO;
using JWTTest.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JWTTest.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AccountsController : ControllerBase
    {

        private readonly DbContextJWT context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly TokenGenerator tokenGenerator;

        public AccountsController(
            DbContextJWT context,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<IdentityUser> signInManager,
            TokenGenerator tokenGenerator)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.tokenGenerator = tokenGenerator;
        }

        [Authorize]
        [HttpGet("test")]
        public async Task<ActionResult<string>> test()
        {
            return "testetstest";
        }

        [Authorize]
        [HttpGet("getusers")]
        public async Task<ActionResult<IEnumerable<IdentityUser>>> GetUsers()
        {
            return await context.Users.ToListAsync();
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDTO>> Login(AccountCredentialsDTO credentials)
        {
            var user = await userManager.FindByNameAsync(credentials.Email);

            if (user == null)
                return Unauthorized();
            if (!await userManager.CheckPasswordAsync(user, credentials.Password))
                return Unauthorized();

            var accessToken = tokenGenerator.GenerateAccessToken(user);


            var response = new AuthResponseDTO
            {
                AccessToken = accessToken,
                UserId = user.Id,
                Username = user.UserName
            };
            return response;
        }

        ///// relese /////

        [HttpPost("register")]
        public async Task<IActionResult> Register(AccountCredentialsDTO credentials)
        {
            var user = new IdentityUser
            {
                Email = credentials.Email,
                UserName = credentials.Email
            };

            var result = await userManager.CreateAsync(user, credentials.Password);

            if (!result.Succeeded)
            {
                return Ok();

            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
    }
}
