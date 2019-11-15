using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Interfaces;
using GoodNews.DB;
using GoodNews.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace GoodNews.Controllers
{

    public class CommentsController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly UserManager<User> _userManager;
        public CommentsController(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _uow= unitOfWork;
            _userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] CommentModel newsComment )
        {
            var commentUser = _userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value).Result;

            var comment = new NewsComment()
            {
                Id = new Guid(),
                User = commentUser,
                News = _uow.NewsRepository.Find(n => n.Id.Equals(newsComment.Id)).FirstOrDefault(),
                Email = commentUser.Email,
                Text = newsComment.commentText
            };
            await _uow.NewsCommentRepository.CreateAsync(comment);
            await _uow.SaveAsync();

            return Json(comment);
            //return RedirectToAction("_GetNewsComments","Comments", new {id = newsComment.Id});
            //return Ok();
           
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteComment([FromBody] Guid id)
        {
            _uow.NewsCommentRepository.Delete(id);
            await _uow.SaveAsync();
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> _GetNewsComments(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = _uow.NewsRepository.Where(p => p.Id.Equals(id)).FirstOrDefault();
            var commentList = await _uow.NewsCommentRepository.Include("User").Include("News").ToListAsync();
            var comments = commentList.Where(i => i.News.Id.Equals(id)).OrderByDescending(o => o.Added);
           

            var vm = new NewsViewModel()
            {
                News = news,
                NewsComments = comments
            };

            return PartialView("_NewsComments", vm);
           
        }
    }
}