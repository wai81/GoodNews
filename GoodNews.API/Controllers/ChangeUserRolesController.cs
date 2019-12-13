using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoodNews.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GoodNews.API.Controllers
{
    [Route("api/[controller]")]
    public class ChangeUserRolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public ChangeUserRolesController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        /// <summary>
        /// GET api/change user roles
        /// </summary>
        /// <returns>Ok(await _roleManager.Roles.ToListAsync())</returns>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                Log.Information("Get all roles was successfully");
                return Ok(await _roleManager.Roles.ToListAsync());
            }
            catch (Exception ex)
            {
                Log.Error($"Get all roles was fail with exception:{Environment.NewLine}{ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// GET api/changeuserroles
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

                    
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var allRoles = _roleManager.Roles.ToList();

                    ChangeRoleViewModel model = new ChangeRoleViewModel
                    {
                        UserId = user.Id,
                        UserEmail = user.Email,
                        UserRoles = userRoles,
                        AllRoles = allRoles
                    };

                    Log.Information("Get change role model was successfully");
                    return Ok(model);
                }
                catch (Exception ex)
                {
                    Log.Error($"Get change role model was fail with exception:{Environment.NewLine}{ex.Message}");
                    return StatusCode(500, "Internal server error");
                }
            }

            return BadRequest();
        }

        /// <summary>
        /// POST api/changeuserroles
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roles"></param>
        /// <returns>Ok()</returns>
        [HttpPost]
        public async Task<ActionResult> Post(string userId, [FromBody] List<string> roles)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                try
                {
                    
                    var user = await _userManager.FindByIdAsync(userId);
                    //===== получем список ролей пользователя =====
                    var userRoles = await _userManager.GetRolesAsync(user);
                    //===== получаем все роли =====
                    var allRoles = _roleManager.Roles.ToList();
                    //===== получаем список ролей, которые были добавлены =====
                    var addedRoles = roles.Except(userRoles);
                    //===== получаем роли, которые были удалены =====
                    var removedRoles = userRoles.Except(roles);

                    await _userManager.AddToRolesAsync(user, addedRoles);
                    await _userManager.RemoveFromRolesAsync(user, removedRoles);

                    Log.Information("Post change role model was successfully");

                    return StatusCode(201, "Post change role model was successfully");
                }
                catch (Exception ex)
                {
                    Log.Error($"Post change role model was fail with exception:{Environment.NewLine}{ex.Message}");

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
