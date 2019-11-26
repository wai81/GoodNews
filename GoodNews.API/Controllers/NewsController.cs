﻿using System;
using System.Linq;
using System.Threading.Tasks;
using GoodNews.API.Models;
using GoodNews.DB;
using GoodNews.Infrastructure.Commands.Models;
using GoodNews.Infrastructure.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

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
        [ProducesResponseType (200,  Type=typeof(SpecialType))]
        [ProducesResponseType (StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get()
        {
            //try
            //{
                return Ok(await mediator.Send(new GetNewsQueryModel()));
            //}
            //catch (Exception e)
            //{
            //    return StatusCode(400,e);
            //}
            
        }


        /// <summary>
        /// Get News by Id
        /// </summary>
        /// <param name="id">id of news articles </param>
        /// <returns></returns>
        // GET api/<controller>/5
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(SpecialType))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var newsDetails = await mediator.Send(new GetNewsByIdQueryModel(id));
                //var newsCategory = await mediator.Send(new GetCategoryByIdQueryModel(newsDetails.CategoryID));
                var newsComments = await mediator.Send(new GetNewsCommentsQueryModel(id));
                newsComments = newsComments.OrderByDescending(c => c.Added);

                var news = new NewsDetailsModel()
                {
                    News = newsDetails,
                    //Category = newsCategory,
                    NewsComments = newsComments
                };
                return Ok(news);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

           
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
       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewsModel news)
        {
            var context = new News()
            { 
                Title = news.Title,
                DateCreate = news.DateCreate,
                NewsContent = news.NewsContent,
                NewsDescription = news.NewsDescription,
                LinkURL = news.LinkURL,
                ImageUrl = news.ImageUrl,
                CategoryID = news.CategoryID
                
            };
            return Ok( await mediator.Send(new AddNewsCommandModel(context)));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            

        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await mediator.Send(new DeleteNewsCommandModel(id)));
        }
    }
}
