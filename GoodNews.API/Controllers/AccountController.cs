using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GoodNews.API.Models.Account;
using GoodNews.DB;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace GoodNews.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration configuration
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }
        /// <summary>
        /// Sign in () 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        //public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

                if (result.Succeeded)
                {
                    Log.Information($"Login operation was successfully: {model.Email}");
                    var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                    //return GenerateJwtToken(model.Email, appUser);
                    var token = GenerateJwtToken(model.Email, appUser);
                    return Ok(token);
                }
                Log.Information($"Login operation was fail: {model.Email}");
                return StatusCode(401, $"Login operation was fail: {Task.FromResult(false)}");
            }
            catch (Exception ex)
            {
                Log.Error($"Login operation was fail with exception: {ex.Message}");
                return StatusCode(500, $"Login operation was fail: {ex.Message}");
            }
        }
        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="model"></param>
        /// <returns>GenerateJwtToken(model.Email, user)</returns>
        [HttpPost]
        //public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                
                var user = new User
                {
                    UserName = model.Email,//UserName,
                    Email = model.Email,
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    Log.Information($"Register operation was successfully: {model.Email}");
                    await _signInManager.SignInAsync(user, false);
                    //return GenerateJwtToken(model.Email, user);
                    var token = GenerateJwtToken(model.Email, user);
                    return Ok(token);
                }
                Log.Information($"Register was fail: {model.Email}");
                return StatusCode(401, $"Register was fail: {Task.FromResult(false)}");
            }
            catch (Exception ex)
            {
                Log.Error($"Register was fail with exception: {ex.Message}");
               return StatusCode(500, $"Register operation was fail: {ex.Message}");
            }
        }

        private string GenerateJwtToken(string email, User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["Jwt:JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["Jwt:JwtIssuer"],
                _configuration["Jwt:JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
