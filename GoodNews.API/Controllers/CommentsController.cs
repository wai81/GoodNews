using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GoodNews.API.Models;
using GoodNews.DB;
using GoodNews.Infrastructure.Commands.Models.Comments;
using GoodNews.Infrastructure.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Serilog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GoodNews.API.Controllers
{
    /// <summary>
    /// CommentController
    /// </summary>

    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CommentsController : Controller
    {
        private readonly IMediator mediator;
        private readonly UserManager<User> userManager;
        public CommentsController(IMediator mediator, UserManager<User> userManager)
        {
            this.mediator = mediator;
            this.userManager = userManager;
        }
       
       /// <summary>
       /// Get Comments by Id News
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
               Log.Information("Get news model by newsId was successfully");
               return Ok(news);
           }
           catch (Exception ex)
           {
                Log.Error($"Get news model by newsId was fail with exception:{Environment.NewLine}{ex.Message}");

                return StatusCode(500, "Internal server error");
            }
       }

       /// <summary>
       /// POST api/comment
       /// </summary>
       /// <param name="text"></param>
       /// <param name="id"></param>
       /// <returns>Ok(comment)</returns>
       [HttpPost]
       public async Task<IActionResult> Post([FromBody] string commentText, Guid id)
       {
            try
            {
                var commentUser = userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value).Result;
                var news = await mediator.Send(new GetNewsByIdQueryModel(id));
                var comment = new NewsComment()
                {
                    Id = new Guid(),
                    User = commentUser,
                    Text = commentText,
                    Email = commentUser.Email,
                    News = news
                };

                await mediator.Send(new AddCommentCommandModel(comment));

                var commentModel = new CommentViewModel()
                {
                    commentId = comment.Id,
                    commentText = comment.Text,
                    newsId = comment.NewsId,
                    usersName = comment.User.UserName,
                    added = comment.Added
                };
                Log.Information("Post new comment  was successfully");
                return StatusCode(201, comment);

            }
            catch (Exception ex)
            {
                Log.Error($"Post new comment was fail with exception:{Environment.NewLine}{ex.Message}");
                return BadRequest();
            }
        }

        //// PUT api/<controller>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}
        
        /// <summary>
        /// DELETE comment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(Guid id)
        {
            try
            {
               await mediator.Send(new DeleteCommentCommandModel(id));
               Log.Information("Delete comment  was successfully");
               return true;
            }
            catch (Exception ex)
            {
                Log.Error($"Delete comment  was fail with exception: {ex.Message}");
                return true;
            }
        }
    }
}
