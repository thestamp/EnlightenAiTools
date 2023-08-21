using Enlighten.Data.Infrastructure;
using Enlighten.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Enlighten.Core.Services
{
    public class TextbookService
    {
        private readonly DataContext _dataContext;

        public TextbookService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Textbook>> GetTextbooks()
        {
            return await Task.FromResult(_dataContext.Textbooks.Include(j => j.Chapters).ToList());
        }

        public async Task<Textbook> GetTextbook(int id)
        {
            return await Task.FromResult(_dataContext.Textbooks.First(x => x.Id == id));
        }

        public async Task<List<TextbookChapter>> GetTextbookChapters(Textbook textbook)
        {
            return await Task.FromResult(_dataContext.TextbookChapters.Where(x => x.Textbook.Id == textbook.Id).ToList());
        }   

        public async Task<TextbookChapter> GetTextbookChapter(Textbook textbook, int id)
        {
            return await Task.FromResult(_dataContext.TextbookChapters.First(x => x.Textbook.Id == textbook.Id && x.Id == id));
        }
      
    }
}
