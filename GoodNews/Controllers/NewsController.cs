using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using GoodNews.DB;
using GoodNews.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using ServiceParser;

namespace GoodNews.Controllers
{
    public class NewsController : Controller
    {
        private readonly IUnitOfWork uow;
        private readonly IHtmlArticleService articleService;
        private readonly UserManager<User> _userManager;
        
        public NewsController(IUnitOfWork unitOfWork, IHtmlArticleService service, UserManager<User> userManager)
        //public NewsController(IUnitOfWork unitOfWork)
         {
            articleService = service;
            uow = unitOfWork;
            _userManager = userManager;
         }
        
        // GET: News
        public async Task<IActionResult> Index()
        {
           // IEnumerable<News> newsList = await uow.NewsRepository.GetAllAsync();

           // HomeViewModel vm = new HomeViewModel()
           //{
           //     News = newsList.ToList()
           //};

            //return View(vm);

            return View(await uow.NewsRepository.GetAllAsync());
        }

        // GET: News/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //var news = await uow.NewsRepository.GetByIdAsync(id);
            var news = uow.NewsRepository.Where(n => n.Id.Equals(id)).FirstOrDefault();
            var commentList = await uow.NewsCommentRepository.Include("User").Include("News").ToListAsync();
            var comments = commentList.Where(n => n.News.Id.Equals(id)).OrderByDescending(n => n.Added);

            if (news == null)
            {
                return NotFound();
            }
            var vm = new NewsViewModel()
            {
                News = news,
               // Category = category,
                NewsComments = comments

            };
            return View(vm);
           //return View(news);
        }


        // GET: News/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(uow.CategoryRepository.GetAll(), "Id", "Name");
            return View();
        }
        // POST: News/Create
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(News news)
        {
            if (ModelState.IsValid)
            {
                uow.NewsRepository.Create(news);
                await uow.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(uow.CategoryRepository.GetAll(), "Id", "Name", news.CategoryID);
            return View(news);
        }

        // GET: News/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var news = await uow.NewsRepository.GetByIdAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(uow.CategoryRepository.GetAll(), "Id", "Name", news.CategoryID);
            return View(news);
        }

        // POST: News/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, News news)
        {
            if (id != news.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    uow.NewsRepository.Update(news);
                    await uow.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(uow.CategoryRepository.GetAll(), "Id", "Name", news.CategoryID);
            return View(news);
        }

        // GET: News/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var news = await uow.NewsRepository.GetByIdAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var news = await uow.NewsRepository.GetByIdAsync(id);
            uow.NewsRepository.Delete(news);
            await uow.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(Guid id)
        {
            var news = uow.NewsRepository.GetByIdAsync(id);
            if (news != null)
                return true;
            return false;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> ParsingNews()
        {
            var parserNews = Task.Factory.StartNew(() =>
                {
                    var pars_S13 = articleService.GetArticlesFrom_S13(@"http://s13.ru/rss");
                    articleService.AddRangeAsync(pars_S13);

                    var pars_TUT = articleService.GetArticlesFrom_TUT(@"https://news.tut.by/rss/all.rss");
                    articleService.AddRange(pars_TUT);

                    var pars_Onliner = articleService.GetArticlesFrom_Onlainer(@"https://people.onliner.by/feed");
                    articleService.AddRange(pars_Onliner);

                }
            );
            parserNews.Wait();
            await uow.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Parsing_Onlainer()
        {
            var parserNews = Task.Factory.StartNew(() =>
                {
                    var pars_Onliner = articleService.GetArticlesFrom_Onlainer(@"https://people.onliner.by/feed");
                    articleService.AddRange(pars_Onliner);
                }
            );
            parserNews.Wait();
            await uow.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Parsing_S13()
        {
            var parserNews = Task.Factory.StartNew(() =>
                {
                    var pars_S13 = articleService.GetArticlesFrom_S13(@"http://s13.ru/rss");
                    articleService.AddRangeAsync(pars_S13);
                   
                }
            );
            parserNews.Wait();
            await uow.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Parsing_TUT()
        {
            var parserNews = Task.Factory.StartNew(() =>
                {
                    var pars_TUT = articleService.GetArticlesFrom_TUT(@"https://news.tut.by/rss/all.rss");
                    articleService.AddRange(pars_TUT);
                }
            );
            parserNews.Wait();
            await uow.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}