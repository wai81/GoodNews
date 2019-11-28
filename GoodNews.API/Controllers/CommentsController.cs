using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GoodNews.API.Models;
using GoodNews.DB;
using GoodNews.Infrastructure.Commands.Models.Comments;
using GoodNews.Infrastructure.Queries.Models.News;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GoodNews.API.Controllers
{
    /// <summary>
    /// CommentController
    /// </summary>

    [Route("api/[controller]")]
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
       /// POST api/comment
       /// </summary>
       /// <param name="text"></param>
       /// <param name="id"></param>
       /// <returns>Ok(comment)</returns>
       [HttpPost]
       public async Task<IActionResult> Post([FromBody] string commentText, Guid id)
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
          
           var commentModel = new CommentModel()
           {
               commentId = comment.Id,
               commentText = comment.Text,
               newsId = comment.NewsId,
               usersName = comment.User.UserName,
               added = comment.Added
           };
            return Ok(commentModel);
       }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        /// <summary>
        /// DELETE comment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(Guid id)
        {
            return (await mediator.Send(new DeleteCommentCommandModel(id)));
        }
    }
}
