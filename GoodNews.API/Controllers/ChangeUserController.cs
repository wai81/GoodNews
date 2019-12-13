using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoodNews.API.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GoodNews.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ChangeUserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public ChangeUserController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// GET api/change user
        /// </summary>
        /// <returns>Ok(await _userManager.Users.ToListAsync())</returns>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                Log.Information("Get all users was successfully");
                return Ok(await _userManager.Users.ToListAsync());
            }
            catch (Exception ex)
            {
                Log.Error($"Get all users was fail with exception:{Environment.NewLine}{ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// GET api/changeuser
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Ok(model)</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string userId)
        {

            if (!string.IsNullOrEmpty(userId))
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    EditUserViewModel model = new EditUserViewModel()
                    {
                        Id = user.Id,
                        Email = user.Email
                    };
                    Log.Information("Get edit user model by userId was successfully");
                    return Ok(model);

                }
                catch (Exception ex)
                {
                    Log.Error($"Get edit user model by userId was fail with exception:{Environment.NewLine}{ex.Message}");
                    return StatusCode(500, "Internal server error");
                }
            }

            return BadRequest();
        }

        /// <summary>
        /// POST api/changeuser
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Ok()</returns>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EditUserViewModel model)
        {
            if (!string.IsNullOrEmpty(model.Id))
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(model.Id);

                    user.Email = model.Email;
                    user.UserName = model.Email;

                    await _userManager.UpdateAsync(user);

                    Log.Information("Update user information was successfully");
                    return StatusCode(201, "Update user information was successfully");
                }
                catch (Exception ex)
                {
                    Log.Error($"Update user information was fail with exception:{Environment.NewLine}{ex.Message}");
                    return StatusCode(500, "Internal server error");
                }
            }

            return BadRequest();
        }

        //// PUT api/<controller>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
