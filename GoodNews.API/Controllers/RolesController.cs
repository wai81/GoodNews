using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GoodNews.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "admin")]
    [ApiController]
    public class RolesController : Controller
    {

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        /// <summary>
        /// GET api/roles
        /// </summary>
        /// <returns>Ok(await _roleManager.Roles.ToListAsync())</returns>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                Log.Information("Get roles was successfully");
                return Ok(await _roleManager.Roles.ToListAsync());
            }
            catch (Exception ex)
            {
                Log.Error($"Get roles was fail with exception: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// GET api/roles
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Ok(_roleManager.Roles.Where(s => s.Id.Equals(id)).ToString())</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            try
            {
                Log.Information("Get roles by id was successfully");
                return Ok(_roleManager.Roles.Where(s => s.Id.Equals(id)).ToString());
            }
            catch (Exception ex)
            {
                Log.Error($"Get roles by id was fail with exception: {ex.Message}");

                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// POST api/roles
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Ok()</returns>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    await _roleManager.CreateAsync(new IdentityRole(value));
                    Log.Information("Create role was successfully");
                    return StatusCode(201, "Create role was successfully");
                }
                catch (Exception ex)
                {
                    Log.Warning($"Create role was fail with exception: {ex.Message}");
                    return StatusCode(500, "Internal server error");
                }
            }

            return BadRequest();
        }

        ///// <summary>
        ///// PUT api/roles
        ///// </summary>
        ///// <param name="userId"></param>
        //[HttpPut("{id}")]
        //public void Put([FromBody] string userId)
        //{
        //}

        /// <summary>
        /// DELETE api/roles
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Ok()</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                try
                {
                    var role = await _roleManager.FindByIdAsync(id);
                    Log.Information("Delete role was successfully");
                    await _roleManager.DeleteAsync(role);
                }
                catch (Exception ex)
                {
                    Log.Error($"Delete role was fail with exception: {ex.Message}");
                    return StatusCode(500, "Internal server error");
                }
            }

            Log.Error("Delete role was fail: string is null or empty");
            return BadRequest();
        }
    }
}
