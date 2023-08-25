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

        public Textbook CreateTextbook()
        {
            return new Textbook()
            {
                Units = new List<TextbookUnit>()
            };
        }

        public async Task AddTextbook(Textbook textbook)
        {
            await _context.AddEntity(textbook);
        }

        public async Task UpdateTextbook(Textbook textbook)
        {
            await _context.UpdateEntity(textbook);
        }


        public async Task DeleteTextbook(Textbook textbook)
        {
            await _context.DeleteEntity(textbook);
        }

        public async Task AddTextbookUnit(Textbook textbook, TextbookUnit unit)
        {
            unit.Textbook = textbook;
            textbook.Units.Add(unit);
            await _context.AddEntity(unit);
        }

        public async Task UpdateTextbookUnit(TextbookUnit unit)
        {
            await _context.UpdateEntity(unit);
        }

        public async Task DeleteTextbookUnit(TextbookUnit unit)
        {
            unit.Textbook.Units.Remove(unit);
            await _context.DeleteEntity(unit);

        }
    }
}
