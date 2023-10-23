using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enlighten.Core.Services;
using Enlighten.Data.Infrastructure;
using Enlighten.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Enlighten.Admin.Core.Services
{
    public class TextbookAdminService : TextbookService
    {
        
        private readonly DataContext _context;

        public TextbookAdminService(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Textbook>> GetTextbooks()
        {
   
            return await Task.FromResult(_context.Textbooks.Include(j => j.Units).ToList());
        }

        public Textbook CreateTextbook()
        {
            return new Textbook()
            {
                Units = new List<TextbookUnit>()
            };
        }

        public void AddTextbook(Textbook textbook)
        {
            _context.AddEntity(textbook);
        }

        public void UpdateTextbook(Textbook textbook)
        {
            _context.UpdateEntity(textbook);
        }


        public void DeleteTextbook(Textbook textbook)
        {
            _context.Attach(textbook);
            _context.DeleteEntity(textbook);
        }

        public TextbookUnit CreateTextbookUnit()
        {
            return new TextbookUnit();
        }

        public void AddTextbookUnit(Textbook textbook, TextbookUnit unit)
        {
            unit.Textbook = textbook;
            textbook.Units.Add(unit);
            _context.UpdateEntity(textbook);
            _context.AddEntity(unit);
        }

        public void UpdateTextbookUnit(TextbookUnit unit)
        {
            _context.UpdateEntity(unit);
        }

        public void DeleteTextbookUnit(TextbookUnit unit)
        {
            unit.Textbook.Units.Remove(unit);
            _context.DeleteEntity(unit);

        }

       
    }
}
