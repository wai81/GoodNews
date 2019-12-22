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
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(email, password, false, false);

                if (result.Succeeded)
                {
                    var user = _userManager.Users.SingleOrDefault(r => r.Email == email);
                    Log.Information($"Login operation was successfully: {email}");
                    return Ok( new
                        {
                            id = user.Id,
                            userName = user.UserName,
                            email = user.Email,
                            roles = await _userManager.GetRolesAsync(user),
                            token = GenerateJwtToken(email, user),
                    });
                }
                Log.Information($"Login operation was fail: {email}");
                return StatusCode(401, $"Login operation was fail: {Task.FromResult(false)}");
            }
            catch (Exception ex)
            {
                Log.Error($"Login operation was fail with exception: {ex.Message}");
                return StatusCode(500, $"Login operation was fail: {ex.Message}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        // <returns>GenerateJwtToken(email, user)</returns>
        [HttpPost]
        //public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        public async Task<IActionResult> Register(string email, [FromBody] string password)
        {
            try
            {
                
                var user = new User
                {
                    UserName = email,
                    Email = email,
                };
                var result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {

                    //await _signInManager.SignInAsync(user, false);
                    await _userManager.AddToRoleAsync(user, "user");

                    Log.Information($"Register operation was successfully: {email}");

                    //var token = GenerateJwtToken(email, user);
                    return Ok(new
                    {
                        id = user.Id,
                        userName = user.UserName,
                        email = user.Email,
                        roles = await _userManager.GetRolesAsync(user),
                        token = GenerateJwtToken(email, user)
                    });
                }
                Log.Information($"Register was fail: {email}");
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
