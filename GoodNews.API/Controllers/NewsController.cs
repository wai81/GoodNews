using System;
using System.Threading.Tasks;
using GoodNews.DB;
using GoodNews.Infrastructure.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GoodNews.API.Controllers
{
    /// <summary>
    /// NewsController
    /// </summary>
   
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class NewsController : ControllerBase
    {
        //private readonly ApplicationContext _context;
        private readonly IMediator mediator;

        // public NewsController(ApplicationContext context)
        public NewsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        /// <summary>
        /// Get all news articles 
        /// </summary>
        /// <returns></returns>
        // GET: api/<controller>
        [HttpGet]
        [ProducesResponseType (StatusCodes.Status200OK)]
        [ProducesResponseType (StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get()
        {
            //return new string[] { "value1", "value2" };
            return Ok(await mediator.Send(new GetNewsQueryModel()));
        }


        /// <summary>
        /// Get News by Id
        /// </summary>
        /// <param name="id">id of news articles </param>
        /// <returns></returns>
        // GET api/<controller>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            //return Ok(_context.News.Where(n => n.Id.Equals(id)).ToArray());
            return Ok(await mediator.Send(new GetNewsByIdQueryModel(id)));
        }
        /// <summary>
        /// Get NewsID by CategoriID
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet("category/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid categoryId)
        {
            return Ok(await mediator.Send(new GetNewsByCategoryIdQueryModel(categoryId)));
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

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
