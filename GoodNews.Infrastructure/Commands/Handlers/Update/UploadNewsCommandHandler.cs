using GoodNews.DB;
using GoodNews.Infrastructure.Commands.Models.Upload;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoodNews.Infrastructure.Commands.Handlers.Upload
{
    class UploadNewsCommandHandler : IRequestHandler<UploadNewsCommandModel, bool>
    {
        private readonly ApplicationContext _context;
        public UploadNewsCommandHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UploadNewsCommandModel request, CancellationToken cancellationToken)
        {
            
            var categoriesList = request.News.Select(n=>n.Category.Name).Distinct().ToList();
            await AddCategories(categoriesList);
            List<News> news = new List<News>();
            foreach (var article in request.News)
            {
                bool newsAny = _context.News.Any(n => n.LinkURL.Equals(article.LinkURL));
                if (newsAny == false)
                {
                    Category category =
                        await _context.Categories.FirstAsync(c => c.Name.Equals(article.Category.Name),
                            cancellationToken);
                    article.Category = category;
                    news.Add(article);
                }
            }
        
            await _context.News.AddRangeAsync(news, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            Log.Information($"UploadNewsCommand -> SUCCSESS. News upload.");
            return true;
        }

        private async Task AddCategories(List<string> categoriesList)
        {
            var categories = _context.Categories.Select(c => c.Name).ToList();
            var notExistingCategories = categoriesList.Except(categories).ToList();
            List<Category> new_Categories = new List<Category>();
            foreach (var name in notExistingCategories)
            {
                Category category = new Category
                {
                    Name = name
                };
                new_Categories.Add(category);
            }

            try
            {
                await _context.Categories.AddRangeAsync(new_Categories);
                await _context.SaveChangesAsync();
                Log.Information($"UploadNewsCommand -> SUCCSESS. Categories upload.");
            }
            catch (Exception e)
            {
                Log.Error($"UploadNewsCommand -> FAILED upload categories: {Environment.NewLine} {e.Message}");
                throw;
            }
        }
    }
}
