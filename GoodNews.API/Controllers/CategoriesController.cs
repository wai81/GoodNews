﻿using System;
using System.Threading.Tasks;
using GoodNews.Infrastructure.Commands.Models.Categories;
using GoodNews.Infrastructure.Queries.Models.Categories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GoodNews.API.Controllers
{
    /// <summary>
    /// CategoriesController
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly IMediator mediator;
        public CategoriesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Get all Catigories
        /// </summary>
        /// <returns></returns>
        // GET: api/<controller>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(SpecialType))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get()
        {
            return Ok(await mediator.Send(new GetCategoriesQueryModel()));
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        /// <summary>
        /// Delete Category by Id
        /// </summary>
        /// <param name="id">id of Category</param>
        /// <returns></returns>
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await mediator.Send(new DeleteCategoryCommandModel(id)));
        }
    }
}