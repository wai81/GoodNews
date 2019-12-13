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
        public async Task<object> Login([FromBody] LoginViewModel model)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

                if (result.Succeeded)
                {
                    //Log.Information("Login operation was successfully");
                    var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                    return GenerateJwtToken(model.Email, appUser);
                }
                Log.Information("Login operation was fail");
                return Task.FromResult(false);
            }
            catch (Exception ex)
            {
                Log.Error($"Login operation was fail with exception: {ex.Message}");
                return Task.FromResult(false);
            }
        }
        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="model"></param>
        /// <returns>GenerateJwtToken(model.Email, user)</returns>
        [HttpPost]
        public async Task<object> Register([FromBody] RegisterViewModel model)
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
                    await _signInManager.SignInAsync(user, false);
                    return GenerateJwtToken(model.Email, user);
                }
                Log.Error("SignInAsync was fail");
                return Task.FromResult(false);
            }
            catch (Exception ex)
            {
                Log.Error($"SignInAsync was fail with exception: {ex.Message}");
                return Task.FromResult(false);
            }
        }

        private object GenerateJwtToken(string email, User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
