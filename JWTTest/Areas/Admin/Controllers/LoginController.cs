using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWTTest.Models;
using JWTTest.Models.DTO;
using JWTTest.Services;
using Microsoft.AspNetCore.Identity;

namespace JWTTest.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly DbContextJWT context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly TokenGenerator tokenGenerator;

        public LoginController(
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


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AccountCredentialsDTO credentials)
        {
            var user = await userManager.FindByNameAsync(credentials.Email);

            if (user == null)
                return Unauthorized();
            if (!await userManager.CheckPasswordAsync(user, credentials.Password))
                return Unauthorized();

            var accessToken = tokenGenerator.GenerateAccessToken(user);



            Response.Cookies.Append("x", accessToken);
            return RedirectToAction("Index","Home");
        }

        
    }
}
