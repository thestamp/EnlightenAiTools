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
            return await Task.FromResult(_dataContext.Textbooks.Include(j => j.Units).ToList());
        }

        public async Task<Textbook> GetTextbook(int id)
        {
            return await Task.FromResult(_dataContext.Textbooks.Include(j => j.Units).First(x => x.Id == id));
        }

        public async Task<List<TextbookUnit>> GetTextbookUnits(Textbook textbook)
        {
            return await Task.FromResult(_dataContext.TextbookUnits.Where(x => x.Textbook.Id == textbook.Id).ToList());
        }   

        public async Task<TextbookUnit> GetTextbookUnit(Textbook textbook, int id)
        {
            return await Task.FromResult(_dataContext.TextbookUnits.First(x => x.Textbook.Id == textbook.Id && x.Id == id));
        }
      
    }
}
