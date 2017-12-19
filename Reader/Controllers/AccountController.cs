using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reader.DataBase;
using Reader.Models;
using Reader.Models.ViewModels;

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

        public AccountController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILoggerFactory loggerFactory)
        {
            _db = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
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
                    return Json(new 
                    {
                        user = user
                    });
                }
                else
                {
                    //throw new Exception(result.ToString());
                    return Unauthorized();
                }
            }
            catch (Exception e)
            {
                _logger.LogWarning(6, "Invalid login attempt.");
                return Unauthorized();
            }
        }

        //
        // POST: /api/Account/register
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    //TODO: Плохо это так оставлять, но сейчас я вертел
                    EmailConfirmed =true
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    user = await _userManager.FindByNameAsync(model.UserName);
                    return Json(user);
                }
            }
            else
            {
                return Json(new { error = "Wrong data. Please, check input and try again" });
            }
            // If we got this far, something failed, redisplay form
            return Json(new {error="Failed to create new user, try again later"});
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
    }
}
