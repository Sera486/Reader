using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Reader.DataBase;
using Reader.Models;
using Reader.ViewModels.Account;

namespace Reader.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public AccountController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILoggerFactory loggerFactory,
            IConfiguration configuration)
        {
            _db = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
            _configuration = configuration;
        }

        //
        // POST: /api/Account/login
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.Remember,
                    lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation(1, "User logged in.");
                    ApplicationUser user = await _userManager.FindByNameAsync(model.UserName);

                    var token = GenerateJwtToken(user.UserName, user);
                    return Ok(new
                    {
                        user = new
                        {
                            id = user.Id,
                            userName = user.UserName,
                            email = user.Email
                        },
                        token
                    });
                }
                else
                {
                    return Json(new {error=result});
                }
            }
            catch (Exception e)
            {
                _logger.LogWarning(6, $"Invalid login attempt.\n{e}");
                return Unauthorized();
            }
        }

        //
        // POST: /api/Account/register
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new {error = "Wrong data. Please, check input and try again"});
            }

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                user = await _userManager.FindByNameAsync(model.UserName);
                var token = GenerateJwtToken(user.UserName, user);
                return Ok(new
                {
                    user = new
                    {
                        id = user.Id,
                        userName = user.UserName,
                        email = user.Email
                    },
                    token
                });
            }
            else return Json(new {error = result.Errors});
        }

        //
        // POST: /api/Account/logout
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation(4, "User logged out.");
            return Json(new
            {
                result = true
            });
        }

        private  string GenerateJwtToken(string userName, ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            IConfigurationSection authConfiguration = _configuration.GetSection("Authorization");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfiguration["TokenKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                authConfiguration["TokenIssuer"],
                authConfiguration["TokenIssuer"],
                claims,
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
