using System;
using System.Linq;
using System.Threading.Tasks;
using Core;
using GoodNews.API.Models;
using GoodNews.DB;
using GoodNews.Infrastructure.Commands.Models.Post;
using GoodNews.Infrastructure.Queries.Models;
using GoodNews.Infrastructure.Queries.Models.Post;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Serilog;

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
        private readonly IMediator mediator;
        private readonly INewsService _newsService;
        public NewsController(IMediator mediator, INewsService newsService)
        {
            
            this.mediator = mediator;
            _newsService = newsService;
        }
        /// <summary>
        /// Get all news articles 
        /// </summary>
        /// <returns></returns>
        // GET: api/<controller>
        [HttpGet]
        [ProducesResponseType (200,  Type=typeof(SpecialType))]
        [ProducesResponseType (StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(int id = 1)
        {
            int countNewsOnPage = 6;
            try
            {
                var countNews = await mediator.Send(new GetNewsCountQueryModel());
                var countPages = (countNews % countNewsOnPage) != 0 ? countNews / countNewsOnPage + 1 : countNews / countNewsOnPage;
                var news = await mediator.Send(new GetNewsPageQueryModel(id, countNewsOnPage));
                news = news.OrderByDescending(s => s.DateCreate);
                NewsModel newsPage = new NewsModel()
                {
                    CountPages = countPages,
                    News = news
                };

                Log.Information("Get all news page was successfully");
                return Ok(newsPage);
            }
            catch (Exception ex)
            {
                Log.Error($"Get all news was fail with exception:{Environment.NewLine}{ex.Message}");
                return StatusCode(500, "Internal server error");
            }

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
                var news = new NewsDetailsViewModel()
                {
                    News = newsDetails,
                    //Category = newsCategory,
                    NewsComments = newsComments
                };
                Log.Information($"Get news by id -> {id} news page was successfully");
                return Ok(news);
            }
            catch (Exception ex)
            {
                Log.Error($"Get news by id -> {id} page was fail with exception:{Environment.NewLine}{ex.Message}");
                return StatusCode(500, "Internal server error");
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
        public async Task<IActionResult> Post([FromBody] NewsViewModel news)
        {
            try
            {
                var context = new News()
                {
                    Title = news.Title,
                    DateCreate = news.DateCreate,
                    NewsContent = news.NewsContent,
                    NewsDescription = news.NewsDescription,
                    LinkURL = news.LinkURL,
                    ImageUrl = news.ImageUrl,
                    CategoryId = news.CategoryID

                };
                Log.Information($"Post news {news} news page was successfully");
                return Ok(await mediator.Send(new AddNewsCommandModel(context)));
            }
            catch (Exception ex)
            {
                Log.Error($"Post news was fail with exception:{Environment.NewLine}{ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        //// PUT api/<controller>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
            

        //}
        /// <summary>
        /// Delete Articel News by Id
        /// </summary>
        /// <param name="id">id of news articles</param>
        /// <returns></returns>
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                Log.Information($"Delete news by id -> {id} was successfully");
                return Ok(await mediator.Send(new DeleteNewsCommandModel(id)));
            }
            catch (Exception ex)
            {
                Log.Error($"Post news was fail with exception:{Environment.NewLine}{ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
